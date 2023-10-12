using BuildingBlocks.Domain.Specification;

namespace Employees.Domain.EmployeeAggregate.Specifications;

public class EmployeeByIdSpecification : Specification<Employee>
{
    public EmployeeByIdSpecification(Guid id)
    {
        AddInclude(x=>x.Role);
        AddFilter(x=>x.Id == id);
    }
}