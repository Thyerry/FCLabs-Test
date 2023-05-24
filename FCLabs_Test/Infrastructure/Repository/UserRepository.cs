using Domain.Interfaces.Repository;
using Domain.Models.ListUser;
using Entities.Entities;
using Entities.Enums;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly DbContextOptions<BaseContext> _options;

    public UserRepository()
    {
        _options = new DbContextOptions<BaseContext>();
    }

    public async Task AddUser(User user)
    {
        using (var database = new BaseContext(_options))
        {
            await database.User.AddAsync(user);
            await database.SaveChangesAsync();
        }
    }

    public async Task<List<User>> ListUsers(ListUsersQueryParameters parameters)
    {
        using (var database = new BaseContext(_options))
        {
            var query = database.User.AsQueryable();

            if (parameters.ReturnActive != parameters.ReturnInactive)
            {
                if (parameters.ReturnActive)
                    query = query.Where(u => u.Status == (int)StatusEnum.ACTIVE);
                else
                    query = query.Where(u => u.Status == (int)StatusEnum.INACTIVE);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Name))
                query = query.Where(u => u.Name.Contains(parameters.Name));

            if (!string.IsNullOrWhiteSpace(parameters.CPF))
                query = query.Where(u => u.CPF == parameters.CPF);

            if (!string.IsNullOrWhiteSpace(parameters.Login))
                query = query.Where(u => u.Login == parameters.Login);

            if (parameters.BirthDateRange != null)
                query = query.Where(u =>
                                u.BirthDate >= parameters.BirthDateRange.Start &&
                                u.BirthDate <= parameters.BirthDateRange.End);

            if (parameters.AgeDateRange != null)
                query = query.Where(u =>
                                u.BirthDate >= parameters.AgeDateRange.Start &&
                                u.BirthDate <= parameters.AgeDateRange.End);

            if (parameters.InclusionDateRange != null)
                query = query.Where(u =>
                                u.InclusionDate >= parameters.InclusionDateRange.Start &&
                                u.InclusionDate <= parameters.InclusionDateRange.End);

            if (parameters.LastChangeDateRange != null)
                query = query.Where(u =>
                                u.LastChangeDate >= parameters.LastChangeDateRange.Start &&
                                u.LastChangeDate <= parameters.LastChangeDateRange.End);

            return query.ToList();
        }
    }

    public async Task UpdateUser(User user)
    {
        using (var database = new BaseContext(_options))
        {
            database.User.Update(user);
            await database.SaveChangesAsync();
        }
    }

    public async Task<User> GetUserByCpf(string cpf)
    {
        using (var database = new BaseContext(_options))
        {
            return await database.User.FirstOrDefaultAsync(u => u.CPF == cpf);
        }
    }

    public async Task Delete(User user)
    {
        using (var database = new BaseContext(_options))
        {
            database.User.Remove(user);
            await database.SaveChangesAsync();
        }
    }

    #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose

    // Flag: Has Dispose already been called?
    private bool disposed = false;

    // Instantiate a SafeHandle instance.
    private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            handle.Dispose();
            // Free any other managed objects here.
            //
        }

        disposed = true;
    }

    #endregion Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
}