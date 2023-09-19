using BuildingBlocks.Domain.Specification;

namespace Products.Domain.MaterialAggregate.Specifications;

public class MaterialCodeDuplicatedSpecification : Specification<Material>
{
    public MaterialCodeDuplicatedSpecification(string materialCode)
    {
        AddFilter(m => m.MaterialCode == materialCode);
    }
}