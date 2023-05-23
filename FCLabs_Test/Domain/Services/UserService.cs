using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
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

        public Task<List<UserModel>> FilterUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetByUserId(string userId)
        {
            var user = await _userRepository.GetByUserId(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<ListUsersResponse> ListUsers(int page)
        {
            var usersToSkip = (page - 1) * usersPerPage;
            var users = await _userRepository.ListUsers(page);
            var totalUsers = users.Count();
            var usersToReturn = users.Skip(usersToSkip).Take(usersPerPage).ToList();

            return new ListUsersResponse()
            {
                Users = _mapper.Map<List<UserModel>>(usersToReturn),
                CurrentPage = page,
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
            await this.UpdateUser(user);
        }

        public async Task Delete(UserModel user)
        {
            var userMapped = _mapper.Map<User>(user);
            await _userRepository.Delete(userMapped);
        }
    }
}