using BuildingBlocks.Domain.Exceptions.Resource;
using Shopping.Domain.ShoppingAggregate;
using Shopping.Domain.Exceptions;

namespace Shopping.Domain.Exceptions;

public class OrderNotFoundException: ResourceNotFoundException
{
    public OrderNotFoundException(Guid id) : base(nameof(Order), id, ErrorCodes.OrderNotFound)
    {
    }
}