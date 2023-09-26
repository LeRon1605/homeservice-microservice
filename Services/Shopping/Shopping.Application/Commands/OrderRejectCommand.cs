using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Commands;

public class OrderRejectCommand : OrderRejectDto,ICommand<OrderDto>
{
    public OrderRejectCommand(OrderRejectDto orderRejectDto)
    {
        Id = orderRejectDto.Id;
    }
}