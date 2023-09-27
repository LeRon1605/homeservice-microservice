using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Roles;

public class PermissionNotFoundException : ResourceNotFoundException
{
    public PermissionNotFoundException(string permission) : base($"Permission {permission} was not found", ErrorCodes.PermissionNotFound) {}
}