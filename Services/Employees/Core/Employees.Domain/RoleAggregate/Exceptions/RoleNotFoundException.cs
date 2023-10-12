using BuildingBlocks.Domain.Exceptions.Resource;

namespace Employees.Domain.RoleAggregate.Exceptions;

public class RoleNotFoundException : ResourceNotFoundException
{
    public RoleNotFoundException(Guid id) : base(nameof(Role), id, ErrorCodes.RoleNotFound)
    {
    }
}

