using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Models.ListUser;
using Entities.Entities;
using Entities.Enums;

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

        public async Task AddUser(UserModel user)
        {
            var cpfValidation = await _userRepository.GetUserByCpf(user.CPF);
            if (cpfValidation != null)
                throw new Exception($"CPF: {user.CPF} already registered");

            var userMapped = _mapper.Map<User>(user);
            userMapped.Status = (int)StatusEnum.ACTIVE;
            userMapped.InclusionDate = DateTime.UtcNow;
            userMapped.LastChangeDate = DateTime.UtcNow;
            await _userRepository.AddUser(userMapped);
        }

        public async Task<UserModel> GetByUserId(string userId)
        {
            var user = await _userRepository.GetByUserId(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<ListUsersResponse> ListUsers(ListUsersRequest request)
        {
            var queryParameters = _mapper.Map<ListUsersQueryParameters>(request);
            queryParameters.AgeDateRange = ParseAgeRange(request.AgeRange);

            var users = await _userRepository.ListUsers(queryParameters);
            var usersOffset = (request.Page - 1) * usersPerPage;
            var totalUsers = users.Count;
            var usersToReturn = users.Skip(usersOffset).Take(usersPerPage).ToList();

            return new ListUsersResponse()
            {
                Users = _mapper.Map<List<UserModel>>(usersToReturn),
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling((decimal)totalUsers / usersPerPage),
                TotalUsers = totalUsers
            };
        }

        public async Task UpdateUser(UserModel user)
        {
            var userMapped = _mapper.Map<User>(user);
            userMapped.LastChangeDate = DateTime.UtcNow;
            await _userRepository.UpdateUser(userMapped);
        }
        public async Task InactivateUser(UserModel user)
        {
            user.Status = (int)StatusEnum.INACTIVE;
            await UpdateUser(user);
        }

        public async Task Delete(UserModel user)
        {
            var userMapped = _mapper.Map<User>(user);
            await _userRepository.Delete(userMapped);
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