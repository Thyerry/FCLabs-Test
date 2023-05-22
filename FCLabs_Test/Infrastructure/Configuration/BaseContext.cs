using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration;

public class BaseContext : IdentityDbContext<ApplicationUser>
{
    public BaseContext(DbContextOptions options) : base(options)
    { }
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

        base.OnModelCreating(builder);
    }

    public string GetConnectionString()
    {
        return "Data Source=DESKTOP-3AH0O3V;Initial Catalog=FCLab_Test;Integrated Security=False;User ID=thyerry;Password=1234;Connection Timeout=15;Encrypt=False;TrustServerCertificate=False";
    }
}