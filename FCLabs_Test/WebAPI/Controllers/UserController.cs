using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> AddUser(UserModel user)
    {
        try
        {
            await _userService.AddUser(user);
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
    public async Task<IActionResult> InactivateUser(UserModel user)
    {
        try
        {
            await _userService.InactivateUser(user);
            return Ok("User was inactivated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> ListUsers(int page)
    {
        try
        {
            var result = await _userService.ListUsers(page);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }
    }
}