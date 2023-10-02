using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos.Orders;

namespace Shopping.Application.Commands.Orders.SubmitOrder;

public class SubmitOrderCommand : ICommand
{
    public IEnumerable<SubmitOrderLineDto> Items { get; set; }
    
    public SubmitOrderCommand(IEnumerable<SubmitOrderLineDto> items)
    {
        Items = items;
    }
}