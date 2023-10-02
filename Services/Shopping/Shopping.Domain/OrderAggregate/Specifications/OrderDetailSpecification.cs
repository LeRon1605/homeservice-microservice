using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class OrderDetailSpecification : Specification<Order>
{
    public OrderDetailSpecification(Guid orderId)
    {
        AddFilter(p => p.Id == orderId);
        AddInclude(x=>x.OrderLines);
    }
}