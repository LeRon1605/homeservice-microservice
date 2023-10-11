using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.MaterialAggregate.Specifications;

public class MaterialByIncludeIdsSpecification : Specification<Material>
{
    public MaterialByIncludeIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(material => ids.Contains(material.Id));
    } 
}