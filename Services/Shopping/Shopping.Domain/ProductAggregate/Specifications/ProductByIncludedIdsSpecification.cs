using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ProductAggregate.Specifications;

public class ProductByIncludedIdsSpecification : Specification<Product>
{
    public ProductByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(product => ids.Contains(product.Id));
        AddInclude(x => x.Reviews);
    }
}