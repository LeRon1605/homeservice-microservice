using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class OrderFilterAndPagingSpecification : Specification<Order>
{
    public OrderFilterAndPagingSpecification(
        string? search,
        int pageIndex,
        int pageSize,
        List<OrderStatus>? status)
    {
        AddInclude(x=>x.OrderLines);
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search.ToLower().Trim());
            AddSearchField("ContactInfo.ContactName");
            //AddFilter(o => o.ContactInfo.ContactName.ToLower().Contains(search.ToLower()));
        }
    
        if (status != null)
        {
            AddFilter(x => status.Contains(x.Status));
        }
    
        ApplyPaging(pageIndex, pageSize);
    }
}