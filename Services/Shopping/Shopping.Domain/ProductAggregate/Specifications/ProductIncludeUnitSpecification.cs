using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ProductAggregate.Specifications;

public class ProductIncludeUnitSpecification: Specification<Product>
{
    public ProductIncludeUnitSpecification()
    {
        AddInclude(x => x.ProductUnit);
    }
}