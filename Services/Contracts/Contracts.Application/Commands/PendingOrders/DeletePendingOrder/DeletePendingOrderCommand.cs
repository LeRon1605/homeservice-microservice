using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.PendingOrders.DeletePendingOrder;

public class DeletePendingOrderCommand: ICommand
{
    public Guid Id { get; set; }

    public DeletePendingOrderCommand(Guid id)
    {
        Id = id;
    }
}