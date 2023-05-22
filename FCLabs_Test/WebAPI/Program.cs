using Microsoft.EntityFrameworkCore;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Domain.Interfaces.Service;
using Domain.Services;
using Domain.Interfaces.Repository;
using Entities.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BaseContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>
    (options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<BaseContext>();

// Dependency injections
// Service
builder.Services.AddSingleton<IUserService, UserService>();

// Repository
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
