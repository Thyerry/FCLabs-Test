using Domain.Models.ListUser;
using Domain.Models.UserModels;

namespace Domain.Interfaces.Service;

public interface IUserService
{
    Task AddUser(UserModel user);

    Task UpdateUser(UserModel user);

    Task Delete(UserModel user);

    Task InactivateUser(UserModel user);

    Task<ListUsersResponse> ListUsers(ListUsersRequest request);

    Task<UserModel> GetByUserCpf(string cpf);
}