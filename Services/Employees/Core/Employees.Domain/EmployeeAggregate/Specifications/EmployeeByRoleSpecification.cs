using BuildingBlocks.Domain.Specification;

namespace Employees.Domain.EmployeeAggregate.Specifications;

public class EmployeeByRoleSpecification : Specification<Employee>
{
    public EmployeeByRoleSpecification(string role)
    {
        AddInclude(x => x.Role);
        AddFilter(x => x.Role.Name == role);
    } 
}