using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.EmployeeAggregate.Exceptions;

public class EmployeeNotFoundException : ResourceNotFoundException
{
    public EmployeeNotFoundException(Guid id) 
        : base(nameof(Employee), id, ErrorCodes.EmployeeNotFound)
    {
    }
}