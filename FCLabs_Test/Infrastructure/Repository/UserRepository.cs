using Domain.Interfaces.Repository;
using Entities.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infrastructure.Repository
{
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

        public async Task Delete(User user)
        {
            using (var database = new BaseContext(_options))
            {
                user.Status = 0;
                database.User.Update(user);
                await database.SaveChangesAsync();
            }
        }

        public async Task<List<User>> FilterUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> ListUsers(int page)
        {
            using (var database = new BaseContext(_options))
            {
                return await database.User.ToListAsync();
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
}