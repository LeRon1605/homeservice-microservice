using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos.Orders;

namespace Shopping.Application.Queries;

public class OrderDetailQuery : IQuery<OrderDetailsDto>
{
    public Guid OrderId { get; private set; }
    public OrderDetailQuery(Guid orderId)
    {
        OrderId = orderId;
    }
}