using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ProductAggregate.Specifications;

public class ProductFilterSpecification : Specification<Product>
{
    public ProductFilterSpecification(string? search, int pageIndex, int pageSize)
    {
        ApplyPaging(pageIndex, pageSize);

        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Product.Name));
        }
    }
}