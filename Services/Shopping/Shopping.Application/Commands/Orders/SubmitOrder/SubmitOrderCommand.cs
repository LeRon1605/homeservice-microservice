using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos.Orders;

namespace Shopping.Application.Commands.Orders.SubmitOrder;

public class SubmitOrderCommand : ICommand
{
    public IEnumerable<OrderLineDto> Items { get; set; }
    
    public SubmitOrderCommand(IEnumerable<OrderLineDto> items)
    {
        Items = items;
    }
}