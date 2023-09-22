using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductByIncludedIdsSpecification : Specification<Product>
{
    public ProductByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(product => ids.Contains(product.Id));
        AddInclude(x => x.Images);
        AddInclude(x => x.Group);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}