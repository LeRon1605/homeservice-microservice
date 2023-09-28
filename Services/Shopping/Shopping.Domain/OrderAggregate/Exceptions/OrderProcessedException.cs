using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.OrderAggregate.Exceptions;

public class OrderProcessedException : ResourceInvalidOperationException
{
    public OrderProcessedException(Guid id) 
        : base($"Order with id '{id}' has already been processed!", ErrorCodes.OrderStatusInvalid)
    {
    }
}