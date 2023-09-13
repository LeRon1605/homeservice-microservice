using BuildingBlocks.Domain.Exceptions.Resource;
using Customers.Domain.CustomerAggregate;

namespace Customers.Domain.Exceptions;

public class CustomerNotFoundException : ResourceNotFoundException
{
    public CustomerNotFoundException(Guid id) : base(nameof(Customer), id, ErrorCodes.CustomerNotFound)
    {
    }
}