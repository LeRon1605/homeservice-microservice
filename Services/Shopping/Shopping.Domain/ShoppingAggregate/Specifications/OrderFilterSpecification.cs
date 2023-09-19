using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ShoppingAggregate.Specifications;

public class OrderFilterSpecification: Specification<Order>
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