using IAC.Application.Dtos.Roles;

namespace IAC.Application.Services.Interfaces;

public interface IRoleService
{
    Task<RoleDto> GetByIdAsync(string id);
}