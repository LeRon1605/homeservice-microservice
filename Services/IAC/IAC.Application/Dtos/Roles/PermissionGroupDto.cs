using IAC.Application.Auth;

namespace IAC.Application.Dtos.Roles;

public class PermissionGroupDto
{
    public List<PermissionInfo> Permissions { get; set; } = new();
}