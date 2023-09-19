using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ShoppingAggregate.Specifications;

public class OrderFilterSpecification: Specification<Order>, ISpecification<Order>
{
    public OrderFilterSpecification(string? search, int pageIndex, int pageSize)
    {
        ApplyPaging(pageIndex, pageSize);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Order.Name));
        }
    }
}