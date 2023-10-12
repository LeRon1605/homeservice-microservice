using BuildingBlocks.Domain.Specification;

namespace Employees.Domain.EmployeeAggregate.Specifications;

public class IsExistingEmployeeEmailSpecification : Specification<Employee>
{
    public IsExistingEmployeeEmailSpecification(string email)
    {
        AddFilter(x=>x.Email==email);
    }
}