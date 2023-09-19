using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class IsProductCodeExistsSpecification : Specification<Product>
{
    public IsProductCodeExistsSpecification(string productCode)
    {
        AddFilter(p => string.Equals(p.ProductCode, productCode, StringComparison.CurrentCultureIgnoreCase));
    }
}