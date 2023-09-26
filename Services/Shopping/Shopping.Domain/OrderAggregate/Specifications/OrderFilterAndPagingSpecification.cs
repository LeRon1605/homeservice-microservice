using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class OrderFilterAndPagingSpecification : Specification<Order>
{
    public OrderFilterAndPagingSpecification(   
        string? search,
        int pageIndex, 
        int pageSize, 
        OrderStatus? status)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Order.ContactName));
        }

        if (status.HasValue)
        {
            AddFilter(x=>x.Status==status);
        }
        
        ApplyPaging(pageIndex,pageSize);
    }
}