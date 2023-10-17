using BuildingBlocks.Domain.Specification;

namespace Employees.Domain.RoleAggregate.Specifications;

public class RoleByIdSpecification : Specification<Role>
{
    public RoleByIdSpecification(Guid id)
    {
        AddFilter(x=>x.Id == id);
    }
}