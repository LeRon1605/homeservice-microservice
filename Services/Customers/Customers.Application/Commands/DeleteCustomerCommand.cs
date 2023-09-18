using BuildingBlocks.Application.CQRS;

namespace Customers.Application.Commands;

public class DeleteCustomerCommand : ICommand
{
    public Guid Id { get; init; }
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}