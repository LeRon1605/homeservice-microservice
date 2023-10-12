using BuildingBlocks.Domain.Exceptions.Resource;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Domain.Exceptions;

public class DuplicateEmployeeEmailException : ResourceAlreadyExistException
{
    public DuplicateEmployeeEmailException(string email) : base(nameof(Employee), nameof(Employee.Email),
        email, ErrorCodes.EmployeeEmailExisting)

    {
    }
}