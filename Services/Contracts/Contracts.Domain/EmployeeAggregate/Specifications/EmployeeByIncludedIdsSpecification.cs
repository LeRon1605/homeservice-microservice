using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class EmployeeByIncludedIdsSpecification : Specification<Employee>
{
    public EmployeeByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(x => ids.Contains(x.Id));
    }
}