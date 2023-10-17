using BuildingBlocks.Application.CQRS;

namespace Employees.Application.Command.Role;

public class RoleCreateCommand : ICommand
{
    public Guid RoleId { get; private set; }
    public string RoleName { get; private set; }

    public RoleCreateCommand(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}