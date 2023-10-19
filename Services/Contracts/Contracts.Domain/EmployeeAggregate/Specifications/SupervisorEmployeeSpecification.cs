using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate.Constants;

namespace Contracts.Domain.EmployeeAggregate.Specifications;

public class SupervisorEmployeeSpecification : Specification<Employee>
{
    public SupervisorEmployeeSpecification()
    {
        AddFilter(x => x.Role == AppEmployee.Supervisor);
    }
}