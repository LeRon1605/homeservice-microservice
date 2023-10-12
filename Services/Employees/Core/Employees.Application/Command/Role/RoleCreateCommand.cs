using BuildingBlocks.Application.CQRS;

namespace Employees.Application.Command.Role;

public class RoleCreateCommand : ICommand
{
    public string RoleId { get; private set; }
    public string RoleName { get; private set; }

    public RoleCreateCommand(string roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}