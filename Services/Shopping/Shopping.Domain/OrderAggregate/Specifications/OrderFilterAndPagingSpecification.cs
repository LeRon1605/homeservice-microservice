using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class OrderFilterAndPagingSpecification : Specification<Order>
{
    // public OrderFilterAndPagingSpecification(
    //     string? search,
    //     int pageIndex,
    //     int pageSize,
    //     List<OrderStatus?> status)
    // {
    //     if (!string.IsNullOrWhiteSpace(search))
    //     {
    //         AddSearchTerm(search);
    //         AddSearchField(nameof(Order.ContactName));
    //     }
    //
    //     if (status.Count != 0)
    //     {
    //         AddFilter(x => status.Contains(x.Status));
    //     }
    //
    //     ApplyPaging(pageIndex, pageSize);
    // }
}