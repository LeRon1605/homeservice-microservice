using BuildingBlocks.Domain.Specification;
using Contracts.Domain.ProductAggregate;

namespace Contracts.Domain.ProductUnitAggregate.Specifications;

public class ProductUnitByIncludedIdsSpecification : Specification<ProductUnit>
{
    public ProductUnitByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(x => ids.Contains(x.Id));
    }
}