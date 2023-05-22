using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Token;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(option =>
             {
                 option.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = config["JWT:Issuer"],
                     ValidAudience = config["JWT:Audience"],
                     IssuerSigningKey = JwtSecurityKey.Create(config["JWT:SecretKey"]!)
                 };

                 option.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                         return Task.CompletedTask;
                     }
                 };
             });

// Dependency injections
// Service
builder.Services.AddSingleton<IUserService, UserService>();

// Repository
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// Mapper
var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserModel>();
    cfg.CreateMap<UserModel, User>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();