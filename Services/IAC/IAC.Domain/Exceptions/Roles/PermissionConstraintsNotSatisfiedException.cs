using BuildingBlocks.Domain.Exceptions.Common;

namespace IAC.Domain.Exceptions.Roles;

public class PermissionConstraintsNotSatisfiedException : InvalidInputException
{
    public PermissionConstraintsNotSatisfiedException(string moduleName) : base(
        $@"View permission is required to grant permission to {moduleName} module", ErrorCodes.PermissionConstraintsNotSatisfied)
    {
    }
}