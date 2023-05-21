using Entities.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task Delete(User user);
    Task<List<User>> ListUsers(int page);
    Task<List<User>> FilterUsers();
}