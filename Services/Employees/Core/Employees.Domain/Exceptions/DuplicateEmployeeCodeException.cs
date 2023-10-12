using BuildingBlocks.Domain.Exceptions.Resource;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Domain.Exceptions;

public class DuplicateEmployeeCodeException : ResourceAlreadyExistException
{
    public DuplicateEmployeeCodeException(string employeeCode) : base(nameof(Employee), nameof(Employee.EmployeeCode),
        employeeCode, ErrorCodes.EmployeeCodeExisting)

    {
    }
}