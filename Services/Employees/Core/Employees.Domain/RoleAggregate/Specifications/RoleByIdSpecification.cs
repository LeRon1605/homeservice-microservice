using BuildingBlocks.Domain.Specification;

namespace Employees.Domain.RoleAggregate.Specifications;

public class RoleByIdSpecification : Specification<Role>
{
    public RoleByIdSpecification(string id)
    {
        AddFilter(x=>x.Id == id);
    }
}