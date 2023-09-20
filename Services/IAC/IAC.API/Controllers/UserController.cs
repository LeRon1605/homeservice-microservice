using IAC.Application.Dtos.Roles;
using IAC.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IAC.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRoleService _roleService;

    public UserController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet("{id}/roles")]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRolesForUserAsync(string id)
    {
        var roles = await _roleService.GetByUserAsync(id);
        return Ok(roles);
    }
}