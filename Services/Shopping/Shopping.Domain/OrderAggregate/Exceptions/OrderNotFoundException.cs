using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.OrderAggregate.Exceptions;

public class OrderNotFoundException : ResourceNotFoundException
{
    public OrderNotFoundException(Guid id) : base(nameof(Order), id, ErrorCodes.OrderNotFound)
    {
        
    }
}