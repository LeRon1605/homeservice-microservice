using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands.Orders.FinishOrder;

public class FinishOrderCommand : ICommand
{
    public Guid Id { get; set; }
    
    public FinishOrderCommand(Guid id)
    {
        Id = id;
    }
}