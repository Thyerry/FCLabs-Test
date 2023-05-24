using Domain.Models;
using Domain.Models.ListUser;

namespace Domain.Interfaces.Service;

public interface IUserService
{
    Task AddUser(UserModel user);

    Task UpdateUser(UserModel user);

    Task Delete(UserModel user);

    Task InactivateUser(UserModel user);

    Task<ListUsersResponse> ListUsers(ListUsersRequest request);

    Task<UserModel> GetByUserId(string id);
}