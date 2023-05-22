using AutoMapper;
using Domain.Interfaces.Service;
using Domain.Models;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Token;

namespace WebAPI.Controllers;

[Route("/api/v1/[controller]")]
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
        if (string.IsNullOrWhiteSpace(login.userName) || string.IsNullOrWhiteSpace(login.password))
            return BadRequest("The user name and the password fields cannot be blank");

        var applicationUser = await _userManager.FindByNameAsync(login.userName);
        if (applicationUser == null)
            return Unauthorized($"A user with the user name {login.userName} doesn't exist");

        var validUser = await _userService.GetByUserId(applicationUser.Id);
        if (validUser.Status == (int)StatusEnum.INACTIVE)
            return Unauthorized("User Inactive");

        var result = await _signInManager.PasswordSignInAsync(login.userName, login.password, false, false);
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
    public async Task<IActionResult> RegisterUserLogin([FromBody] UserModel user)
    {
        if (string.IsNullOrWhiteSpace(user.Login)
            || string.IsNullOrWhiteSpace(user.Email)
            || string.IsNullOrWhiteSpace(user.ConfirmPassword)
            || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest("Some of the fields are empty");

        if (user.Password != user.ConfirmPassword)
            return BadRequest("Passwords don't match");

        var newUser = new ApplicationUser
        {
            UserName = user.Login,
            Email = user.Email,
            CPF = user.CPF,
        };

        try
        {
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var userRegistered = await _userManager.FindByEmailAsync(user.Email);
            user.UserId = userRegistered.Id;
            await _userService.AddUser(user);
            return Ok("User Registered Successfully");
        }
        catch (Exception ex)
        {
            await _userService.Delete(user);
            await _userManager.DeleteAsync(newUser);
            return BadRequest(ex.Message);
        }

    }
}