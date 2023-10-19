using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate.Constants;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class CustomerServiceEmployeeSpecification : Specification<Employee>
{
    public CustomerServiceEmployeeSpecification()
    {
        AddFilter(x => x.Role == AppEmployee.CustomerService);
    }
}