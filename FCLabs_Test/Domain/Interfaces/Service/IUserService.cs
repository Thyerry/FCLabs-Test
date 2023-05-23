using Domain.Models;

namespace Domain.Interfaces.Service;

public interface IUserService
{
    Task AddUser(UserModel user);

    Task UpdateUser(UserModel user);

    Task Delete(UserModel user);

    Task InactivateUser(UserModel user);

    Task<ListUsersResponse> ListUsers(int page);

    Task<List<UserModel>> FilterUsers();

    Task<UserModel> GetByUserId(string id);
}