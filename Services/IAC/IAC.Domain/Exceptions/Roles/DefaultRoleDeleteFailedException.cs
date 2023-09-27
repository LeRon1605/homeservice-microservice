using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Roles;

public class DefaultRoleDeleteFailedException : ResourceInvalidOperationException
{
    public DefaultRoleDeleteFailedException(string name) 
        : base($"Can not delete default role '{name}'", ErrorCodes.DefaultRoleDeleteFailed)
    {
    }
}