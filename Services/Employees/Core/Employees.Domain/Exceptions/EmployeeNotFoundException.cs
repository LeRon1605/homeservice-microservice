using BuildingBlocks.Domain.Exceptions.Resource;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Domain.Exceptions;

public class EmployeeNotFoundException : ResourceNotFoundException
{
    public EmployeeNotFoundException(Guid id) : base(nameof(Employee), id, ErrorCodes.EmployeeNotFound)
    {
    }
}