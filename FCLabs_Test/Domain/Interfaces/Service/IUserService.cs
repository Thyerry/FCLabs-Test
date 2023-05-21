using Entities.Entities;

namespace Domain.Interfaces.Service;

public interface IUserService
{
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task Delete(User user);
    Task<List<User>> ListUsers(int page);
    Task<List<User>> FilterUsers();
}