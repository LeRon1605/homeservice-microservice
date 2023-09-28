using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos;

namespace Shopping.Application.Commands.RejectOrder;

public class OrderRejectCommand : OrderRejectDto,ICommand<OrderDto>
{
    public OrderRejectCommand(OrderRejectDto orderRejectDto)
    {
        Id = orderRejectDto.Id;
    }
}