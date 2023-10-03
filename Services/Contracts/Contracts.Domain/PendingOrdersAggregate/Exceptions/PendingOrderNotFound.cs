using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.PendingOrdersAggregate.Exceptions;

public class PendingOrderNotFound : ResourceNotFoundException
{
    public PendingOrderNotFound(Guid id) 
        : base($"There are currently no order with id '{id}' is in pending status!",ErrorCodes.PendingOrderNotFound)
    {
    }
}