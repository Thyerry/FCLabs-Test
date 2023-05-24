using Domain.Models.ListUser;
using Entities.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task Delete(User user);
    Task<List<User>> ListUsers(ListUsersQueryParameters parameters);
    Task<User> GetByUserId(string userId);
    Task<User> GetUserByCpf(string cpf);
}