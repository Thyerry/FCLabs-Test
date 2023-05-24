using Domain.Models.UserModels;
using Domain.Models.UserModels.ListUser;
using Entities.Enums;

namespace Domain.Interfaces.Service;

public interface IUserService
{
    Task AddUser(AddUserRequest user);

    Task UpdateUser(UserModel user);

    Task Delete(UserModel user);

    Task InactivateUser(int userId);

    Task InactivateUsersBatch(List<int> userId);

    Task<ListUsersResponse> ListUsers(ListUsersRequest request);

    Task<UserModel> GetUserByCpf(string cpf);

    Task<ExportListUsersResult> ExportListUsers(ExportListUsersRequest request);
}