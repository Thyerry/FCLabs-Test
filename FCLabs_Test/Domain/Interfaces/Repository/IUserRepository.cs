using Domain.Models.UserModels.ListUser;
using Entities.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task Delete(User user);
    Task<List<User>> ListUsers(ListUsersQueryParameters parameters);
    Task<User> GetUserByCpf(string cpf);
    Task<User> GetUserById(int? id);
    Task<List<User>> GetUsersById(List<int> ids);
}