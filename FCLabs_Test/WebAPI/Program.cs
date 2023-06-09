using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models.LoginModels;
using Domain.Models.UserModels;
using Domain.Models.UserModels.ListUser;
using Domain.Services;
using Entities.Entities;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Token;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database

builder.Services.AddDbContext<BaseContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>
    (options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<BaseContext>();

#endregion Database

#region Jwt

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

#endregion Jwt

#region Dependency injections

// Service
builder.Services.AddSingleton<IUserService, UserService>();

// Repository
builder.Services.AddSingleton<IUserRepository, UserRepository>();

#endregion Dependency injections

#region Mapper

var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserModel>();
    cfg.CreateMap<UserModel, User>();
    cfg.CreateMap<User, UserResponse>();
    cfg.CreateMap<UserResponse, User>();
    cfg.CreateMap<User, AddUserRequest>();
    cfg.CreateMap<AddUserRequest, User>();
    cfg.CreateMap<RegisterRequest, AddUserRequest>();
    cfg.CreateMap<AddUserRequest, RegisterRequest>();
    cfg.CreateMap<UserModel, RegisterRequest>();
    cfg.CreateMap<RegisterRequest, UserModel>();
    cfg.CreateMap<ListUsersQueryParameters, ListUsersRequest>();
    cfg.CreateMap<ListUsersRequest, ListUsersQueryParameters>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion Mapper

#region FluentValidation

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(UserService).Assembly);
});

#endregion FluentValidation

#region Cors

builder.Services.AddCors();

#endregion Cors

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();