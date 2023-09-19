using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Roles;

public class RoleNotFoundException : ResourceNotFoundException
{
    public RoleNotFoundException(string id) : base("Role", "Id", id,ErrorCodes.RoleNotFound)
    {
    }
    
    public RoleNotFoundException(string column, string value) : base($"Role with {column} '{value}' does not exist!", ErrorCodes.RoleNotFound)
    {
    }
}