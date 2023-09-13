using IAC.Application.Dtos.Roles;
using IAC.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IAC.API.Controllers;

[Route("api/roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleByIdAsync(string id)
    {
        var role = await _roleService.GetByIdAsync(id);
        return Ok(role);
    }
}