using IAC.Application.Dtos.Roles;

namespace IAC.Application.Dtos.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public IEnumerable<RoleDto> Roles { get; set; } = null!;
    public IEnumerable<string> Permissions { get; set; } = null!;
}