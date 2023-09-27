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
    
    [HttpGet("permissions")]
    [ProducesResponseType(typeof(IEnumerable<PermissionGroupDto>), StatusCodes.Status200OK)]
    public IActionResult GetAllPermissions()
    {
        var permissions = _roleService.GetAllPermissions();
        return Ok(permissions);
    }

    [HttpGet("{id:guid}/permissions")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPermissionsInRole(Guid id)
    {
        var permissions = await _roleService.GetPermissionsInRoleAsync(id.ToString());
        return Ok(permissions);
    }
    
    [HttpPut("{id:guid}/permissions")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditPermissionsInRole(Guid id, IEnumerable<string> permissions)
    {
        await _roleService.EditPermissionsInRoleAsync(id.ToString(), permissions.ToList());
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRolesAsync()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }
    
    [HttpGet("{id}")]
    [ActionName(nameof(GetRoleByIdAsync))]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleByIdAsync(string id)
    {
        var role = await _roleService.GetByIdAsync(id);
        return Ok(role);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRoleAsync(RoleCreateDto dto)
    {
        var role = await _roleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetRoleByIdAsync), new { id = role.Id }, role);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRoleAsync(string id, RoleUpdateDto dto)
    {
        var role = await _roleService.UpdateAsync(id, dto);
        return Ok(role);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRoleAsync(string id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }
}