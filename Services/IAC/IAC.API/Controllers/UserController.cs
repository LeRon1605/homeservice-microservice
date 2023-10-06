using System.Security.Claims;
using IAC.Application.Dtos.Roles;
using IAC.Application.Dtos.Users;
using IAC.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAC.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IAuthenticateService _authService;

    public UserController(IRoleService roleService, IAuthenticateService authenticateService)
    {
        _roleService = roleService;
        _authService = authenticateService;
    }
    
    [HttpGet("{id}/roles")]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRolesForUserAsync(string id)
    {
        var roles = await _roleService.GetByUserAsync(id);
        return Ok(roles);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserInfoAsync()
    {
        var userInfo = await _authService.GetCurrentUserInfoAsync();
        return Ok(userInfo);
    }
}