using System.Text.Json;
using Application.Helper;
using Domain.DTOs.Notes;
using Domain.DTOs.Users;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUsersService _usersServices;

    public AuthController(IUsersService usersServices)
    {
        _usersServices = usersServices;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDTO request)
    {
        return Ok(await _usersServices.Create(request));
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDTO request)
    {
        var user = await _usersServices.Authenticate(request);

        return Ok(AuthHelper.GenerateJwtToken(user));
    }
}