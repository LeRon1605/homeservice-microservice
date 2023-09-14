using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductsWithPaginationSpec : Specification<Product>
{
    private readonly string? _search;
    
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return string.IsNullOrWhiteSpace(_search)
                   ? p => true
                   : p => p.Name != null && p.Name.ToLower().Contains(_search.ToLower());
    }

    public ProductsWithPaginationSpec(string? search, int pageIndex, int pageSize)
    {
        _search = search;
        
        ApplyPaging(pageIndex, pageSize);
        AddInclude(x => x.Group);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}