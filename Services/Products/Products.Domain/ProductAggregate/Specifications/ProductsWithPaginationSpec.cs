using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductsWithPaginationSpec : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return p => true;
    }

    public ProductsWithPaginationSpec(string? search, int pageIndex, int pageSize)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Product.Name));
        }
        ApplyPaging(pageIndex, pageSize);
    }
}