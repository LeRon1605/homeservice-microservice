using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Roles;

public class RoleNotFoundException : ResourceNotFoundException
{
    public RoleNotFoundException(string id) : base("Role", "Id", id,ErrorCodes.RoleNotFound)
    {
    }
}