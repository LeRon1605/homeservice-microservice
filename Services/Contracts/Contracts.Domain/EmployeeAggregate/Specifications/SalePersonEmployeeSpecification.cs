using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate.Constants;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class SalePersonEmployeeSpecification : Specification<Employee>
{
    public SalePersonEmployeeSpecification()
    {
        AddFilter(x => x.Role == AppEmployee.SalesPerson);
    }
}