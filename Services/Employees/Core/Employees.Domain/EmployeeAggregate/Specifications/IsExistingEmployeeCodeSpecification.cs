using BuildingBlocks.Domain.Specification;

namespace Employees.Domain.EmployeeAggregate.Specifications;

public class IsExistingEmployeeCodeSpecification : Specification<Employee>
{
    public IsExistingEmployeeCodeSpecification(int employeeCode)
    {
        AddFilter(x => x.EmployeeCode == employeeCode);
    }
}