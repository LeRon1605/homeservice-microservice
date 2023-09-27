using IAC.Application.Auth;
using IAC.Application.Dtos.Roles;

namespace IAC.Application.Services.Interfaces;

public interface IRoleService
{
    IEnumerable<PermissionInfo> GetAllPermissions();
    
    Task<IEnumerable<string>> GetPermissionsInRoleAsync(string roleId);
    Task EditPermissionsInRoleAsync(string roleId, IList<string> permissions);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    
    Task<RoleDto> GetByIdAsync(string id);

    Task<RoleDto> CreateAsync(RoleCreateDto dto);

    Task<RoleDto> UpdateAsync(string id, RoleUpdateDto dto);

    Task DeleteAsync(string id);

    Task<IEnumerable<RoleDto>> GetByUserAsync(string userId);
}