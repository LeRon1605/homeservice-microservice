using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate.Constants;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class IsCustomerServiceEmployeeSpecification : Specification<Employee>
{
    public IsCustomerServiceEmployeeSpecification(Guid id)
    {
        AddFilter(x => x.Id == id && x.Role == AppEmployee.CustomerService);
    }
}