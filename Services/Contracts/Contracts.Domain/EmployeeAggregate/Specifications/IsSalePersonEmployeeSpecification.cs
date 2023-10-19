using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate.Constants;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class IsSalePersonEmployeeSpecification : Specification<Employee>
{
    public IsSalePersonEmployeeSpecification(Guid employeeId)
    {
        AddFilter(x => x.Id == employeeId && x.Role == AppEmployee.SalesPerson);
    }
}