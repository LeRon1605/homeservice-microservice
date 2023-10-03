using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.CustomerAggregate.Exceptions;

public class CustomerNotFoundException : ResourceNotFoundException
{
    public CustomerNotFoundException(Guid id) : base(nameof(Customer), id, ErrorCodes.CustomerNotFound)
    {
    }
}