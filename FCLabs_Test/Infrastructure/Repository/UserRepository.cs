using Domain.Interfaces.Repository;
using Entities.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextOptions<BaseContext> _options;

        public UserRepository(DbContextOptions<BaseContext> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FilterUsers()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> ListUsers(int page)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}