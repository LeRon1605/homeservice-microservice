using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ProductAggregate.Specifications;

public class ProductByIncludedIdsSpecification : Specification<Product>
{
    public ProductByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(product => ids.Contains(product.Id));
    }
}