using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Roles;

public class RoleHasGrantedToUserException : ResourceInvalidOperationException
{
    public RoleHasGrantedToUserException(string id) 
        : base($"Role with id '{id}' has already been granted to users!", ErrorCodes.RoleHasGrantedToUser)
    {
    }
}