using Domain.Interfaces.Service;
using Domain.Models.UserModels;
using Domain.Models.UserModels.ListUser;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

//[Authorize]
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> AddUser(AddUserRequest request)
    {
        try
        {
            await _userService.AddUser(request);
            return Ok("User Added");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }

    [HttpPut]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateUser(UserModel user)
    {
        try
        {
            await _userService.UpdateUser(user);
            return Ok("User informarion Updated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }

    [HttpDelete]
    [Produces("application/json")]
    public async Task<IActionResult> InactivateUser([FromQuery]int userId)
    {
        try
        {
            await _userService.InactivateUser(userId);
            return Ok("User was inactivated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }

    [HttpDelete]
    [Route("/User/Batch")]
    [Produces("application/json")]
    public async Task<IActionResult> InactivateUsersBatch(List<int> userId)
    {
        try
        {
            await _userService.InactivateUsersBatch(userId);
            return Ok("Users were inactivated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> ListUsers([FromQuery] ListUsersRequest request)
    {
        try
        {
            var result = await _userService.ListUsers(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }
}