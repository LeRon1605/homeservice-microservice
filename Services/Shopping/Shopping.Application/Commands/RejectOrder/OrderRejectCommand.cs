using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos;
using Shopping.Application.Dtos.Orders;

namespace Shopping.Application.Commands.RejectOrder;

public class OrderRejectCommand : OrderRejectDto,ICommand<OrderDto>
{
    public OrderRejectCommand(OrderRejectDto orderRejectDto)
    {
        Id = orderRejectDto.Id;
    }
}