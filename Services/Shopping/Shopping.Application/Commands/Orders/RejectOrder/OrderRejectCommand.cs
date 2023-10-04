using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos.Orders;

namespace Shopping.Application.Commands.Orders.RejectOrder;

public class OrderRejectCommand : OrderRejectDto, ICommand<OrderDto>
{
    public Guid Id { get; set; }

    public OrderRejectCommand(Guid id, OrderRejectDto orderRejectDto)
    {
        Id = id;
        Description = orderRejectDto.Description;
    }
}