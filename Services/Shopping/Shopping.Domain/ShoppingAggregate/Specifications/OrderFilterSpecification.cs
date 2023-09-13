using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ShoppingAggregate.Specifications;

public class OrderFilterSpecification: Specification<Order>, ISpecification<Order>
{
    private readonly string? _search;
    
    public OrderFilterSpecification(string? search, int pageIndex, int pageSize)
    {
        _search = search;
        ApplyPaging(pageIndex, pageSize);
    }

    public override Expression<Func<Order, bool>> ToExpression()
    {
        return string.IsNullOrWhiteSpace(_search)
            ? p => true
            : p => p.Name.ToLower().Contains(_search.ToLower());
    }
}