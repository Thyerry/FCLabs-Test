﻿using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models.UserModels;
using Domain.Models.UserModels.ListUser;
using Domain.Utils;
using Entities.Entities;
using Entities.Enums;
using FluentValidation;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private int usersPerPage = 10;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddUser(AddUserRequest user)
        {
            var cpfValidation = await _userRepository.GetUserByCpf(user.CPF);
            if (cpfValidation != null)
                throw new Exception($"CPF: {user.CPF} already registered");

            var userMapped = _mapper.Map<User>(user);
            userMapped.Status = StatusEnum.ACTIVE;
            userMapped.InclusionDate = DateTime.UtcNow;
            userMapped.LastChangeDate = DateTime.UtcNow;
            await _userRepository.AddUser(userMapped);
        }

        public async Task<UserModel> GetUserByCpf(string cpf)
        {
            var user = await _userRepository.GetUserByCpf(cpf);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<ListUsersResponse> ListUsers(ListUsersRequest request)
        {
            var queryParameters = MapRequestIntoQuery(request);
            var users = await _userRepository.ListUsers(queryParameters);
            var usersOffset = (request.Page - 1) * usersPerPage;
            var totalUsers = users.Count;
            var usersToReturn = users.Skip(usersOffset).Take(usersPerPage).ToList();

            return new ListUsersResponse()
            {
                Users = _mapper.Map<List<UserResponse>>(usersToReturn),
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling((decimal)totalUsers / usersPerPage),
                TotalUsers = totalUsers
            };
        }

        public async Task UpdateUser(UserModel user)
        {
            var currentUser = await GetUserById(user.Id);

            if (currentUser.CPF != user.CPF ||
                currentUser.Email != user.Email ||
                currentUser.Login != user.Login ||
                currentUser.BirthDate != user.BirthDate
                )
                throw new ValidationException("Trying to change one or more of the following fields that can't be changed: CPF, Email, Login, Birth Date");

            var userMapped = _mapper.Map<User>(user);
            userMapped.LastChangeDate = DateTime.UtcNow;
            await _userRepository.UpdateUser(userMapped);
        }

        public async Task<User> GetUserById(int? Id)
        {
            return await _userRepository.GetUserById(Id);
        }

        public async Task InactivateUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new ValidationException("User not found!");

            user.Status = StatusEnum.INACTIVE;
            user.LastChangeDate = DateTime.UtcNow;
            await _userRepository.UpdateUser(user);
        }

        public async Task InactivateUsersBatch(List<int> ids)
        {
            var users = await _userRepository.GetUsersById(ids);
            if (!users.Any())
                throw new ValidationException("Users not found!");

            users.ForEach(async u =>
            {
                u.Status = StatusEnum.INACTIVE;
                u.LastChangeDate = DateTime.UtcNow;
                await _userRepository.UpdateUser(u);
            });
        }

        public async Task Delete(UserModel user)
        {
            var userMapped = _mapper.Map<User>(user);
            await _userRepository.Delete(userMapped);
        }

        public async Task<ExportListUsersResult> ExportListUsers(ExportListUsersRequest request)
        {
            var queryParameters = MapRequestIntoQuery(request);
            var users = await _userRepository.ListUsers(queryParameters);
            if (!users.Any())
                throw new ValidationException("The query didn't return any user");

            return FileUtils.ExportListUsers(users, request.ExportFormat);
        }

        private ListUsersQueryParameters MapRequestIntoQuery(ListUsersRequest request)
        {
            var queryParams = _mapper.Map<ListUsersQueryParameters>(request);

            queryParams.AgeDateRange = ParseAgeRange(request.AgeRange);
            queryParams.BirthDateRange = DateTimeRangeValidation(request.BirthDateRangeStart, request.BirthDateRangeEnd);
            queryParams.InclusionDateRange = DateTimeRangeValidation(request.InclusionDateRangeStart, request.InclusionDateRangeEnd);
            queryParams.LastChangeDateRange = DateTimeRangeValidation(request.LastChangeDateRangeStart, request.LastChangeDateRangeEnd);

            return queryParams;
        }

        private DateTimeRange? DateTimeRangeValidation(DateTime? start, DateTime? end)
        {
            if (start != null || end != null)
                return new DateTimeRange
                {
                    Start = start ?? DateTime.MinValue,
                    End = end ?? DateTime.MaxValue,
                };
            else
                return null;
        }

        private DateTimeRange? ParseAgeRange(AgeRangeEnum? ageRange)
        {
            if (ageRange == null)
                return null;
            switch (ageRange)
            {
                case AgeRangeEnum.NineteenToTwentyfive:
                    return new DateTimeRange
                    {
                        Start = DateTime.UtcNow.AddYears(-25),
                        End = DateTime.UtcNow.AddYears(-19),
                    };

                case AgeRangeEnum.TwentysixToThirty:
                    return new DateTimeRange
                    {
                        Start = DateTime.UtcNow.AddYears(-30),
                        End = DateTime.UtcNow.AddYears(-26),
                    };

                case AgeRangeEnum.ThirtyoneToThirtyfive:
                    return new DateTimeRange
                    {
                        Start = DateTime.UtcNow.AddYears(-35),
                        End = DateTime.UtcNow.AddYears(-31),
                    };

                case AgeRangeEnum.ThirtysixTotFourty:
                    return new DateTimeRange
                    {
                        Start = DateTime.UtcNow.AddYears(-40),
                        End = DateTime.UtcNow.AddYears(-36),
                    };

                case AgeRangeEnum.AboveForty:
                    return new DateTimeRange
                    {
                        Start = DateTime.MinValue,
                        End = DateTime.UtcNow.AddYears(-41),
                    };

                default:
                    return null;
            }
        }
    }
}