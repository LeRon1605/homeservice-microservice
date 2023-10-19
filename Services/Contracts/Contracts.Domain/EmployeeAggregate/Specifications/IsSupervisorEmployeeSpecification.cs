using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate.Constants;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class IsSupervisorEmployeeSpecification : Specification<Employee>
{
    public IsSupervisorEmployeeSpecification(Guid id)
    {
        AddFilter(x => x.Id == id && x.Role == AppEmployee.Supervisor);
    }
}