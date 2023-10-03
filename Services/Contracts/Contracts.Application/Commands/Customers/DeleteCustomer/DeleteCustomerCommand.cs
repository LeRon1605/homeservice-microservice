using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Customers.DeleteCustomer;

public class DeleteCustomerCommand : ICommand
{
    public Guid Id { get; init; }
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}