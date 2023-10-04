using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands;

public class RejectOrderCommand: ICommand
{
    public Guid Id { get; set; }

    public RejectOrderCommand(Guid id)
    {
        Id = id;
    }
}