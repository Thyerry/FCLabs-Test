using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration;

public class BaseContext : IdentityDbContext<IdentityUser>
{
    public BaseContext(DbContextOptions options) : base(options)
    { }
    public DbSet<User> User { get; set; }
}