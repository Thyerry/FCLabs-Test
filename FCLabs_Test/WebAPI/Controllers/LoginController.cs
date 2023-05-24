using AutoMapper;
using Domain.Interfaces.Service;
using Domain.Models.LoginModels;
using Domain.Models.UserModels;
using Domain.Models.UserModels.ListUser;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Token;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly string tokenSecretKey = "this-is-the-secret-key-for-this-example";
    private readonly int ADayInMinutes = 1445;

    public LoginController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserService userService,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("CreateLoginToken")]
    public async Task<IActionResult> CreateLoginToken([FromBody] LoginRequest login)
    {
        if (string.IsNullOrWhiteSpace(login.cpf) || string.IsNullOrWhiteSpace(login.password))
            return BadRequest("The user name and the password fields cannot be blank");

        var applicationUser = await _userManager.FindByCpfAsync(login.cpf);
        if (applicationUser == null)
            return Unauthorized($"A user with the user name {login.cpf} doesn't exist");

        var validUser = await _userService.GetUserByCpf(applicationUser.CPF);
        if (validUser.Status == StatusEnum.INACTIVE)
            return Unauthorized("User Inactive");

        var result = await _signInManager.PasswordSignInAsync(applicationUser.UserName, login.password, false, false);
        if (!result.Succeeded)
            return Unauthorized("Wrong password");

        var token = new TokenJwtBuilder()
            .AddSecurityKey(JwtSecurityKey.Create(tokenSecretKey))
            .AddSubject("FCLabs challenge")
            .AddIssuer("Thyerry.Nunes")
            .AddAudience("FCLab.Recruit.Team")
            .AddClaim("UserApi", "1")
            .AddExpiry(ADayInMinutes)
            .Builder();

        return Ok(token.value);
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("RegisterUserLogin")]
    public async Task<IActionResult> RegisterUserLogin([FromBody] RegisterRequest request)
    {
        var newUser = new ApplicationUser
        {
            UserName = request.Login,
            Email = request.Email,
            CPF = request.CPF,
        };

        var user = _mapper.Map<AddUserRequest>(request);
        try
        {
            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var userExists = await _userService.GetUserByCpf(request.CPF);
            if(userExists == null)
                await _userService.AddUser(user);

            return Ok("User Registered Successfully");
        }
        catch (Exception ex)
        {
            await _userManager.DeleteAsync(newUser);
            return BadRequest(ex.Message);
        }

    }
}