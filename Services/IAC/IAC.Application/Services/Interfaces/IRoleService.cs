using IAC.Application.Dtos.Roles;

namespace IAC.Application.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    
    Task<RoleDto> GetByIdAsync(string id);

    Task<RoleDto> CreateAsync(RoleCreateDto dto);

    Task<RoleDto> UpdateAsync(string id, RoleUpdateDto dto);

    Task DeleteAsync(string id);

    Task<IEnumerable<RoleDto>> GetByUserAsync(string userId);
}