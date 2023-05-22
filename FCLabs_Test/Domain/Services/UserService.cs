using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Entities.Entities;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private int usersPerPage = 10;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task AddUser(User user)
        {
            await _userRepository.AddUser(user);
        }

        public async Task Delete(User user)
        {
            await _userRepository.Delete(user);
        }

        public Task<List<User>> FilterUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUserId(string userId)
        {
            return await _userRepository.GetByUserId(userId);
        }

        public async Task<List<User>> ListUsers(int page)
        {
            var usersToSkip = page * usersPerPage;
            var users = await _userRepository.ListUsers(page);
            return users.Skip(usersToSkip).Take(usersPerPage).ToList();
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }
    }
}