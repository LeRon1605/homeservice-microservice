using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.MaterialAggregate.Specifications;

public class GetMaterialsByIdsSpecification : Specification<Material>
{
    public GetMaterialsByIdsSpecification(IEnumerable<Guid> materialIds)
    {
        AddFilter(material => materialIds.Contains(material.Id));
    } 
}