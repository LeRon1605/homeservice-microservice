using BuildingBlocks.Application.CQRS;

namespace Customers.Application.Commands.DeleteCustomer;

public class DeleteCustomerCommand : ICommand
{
    public Guid Id { get; init; }
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}