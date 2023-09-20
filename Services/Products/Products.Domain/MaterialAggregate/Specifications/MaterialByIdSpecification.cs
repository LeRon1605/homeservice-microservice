using BuildingBlocks.Domain.Specification;

namespace Products.Domain.MaterialAggregate.Specifications;

public class MaterialByIdSpecification : Specification<Material>
{
    public MaterialByIdSpecification(Guid productId)
    {
        AddFilter(p => p.Id == productId);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}