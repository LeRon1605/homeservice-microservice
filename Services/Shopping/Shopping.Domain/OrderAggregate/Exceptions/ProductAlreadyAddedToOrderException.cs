using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.OrderAggregate.Exceptions;

public class ProductAlreadyAddedToOrderException : ResourceAlreadyExistException
{
    public ProductAlreadyAddedToOrderException(Guid productId, string color) 
        : base($"Product with id '{productId}' having the same color '{color}' has already been added to order!", ErrorCodes.ProductAlreadyAddedToOrder)
    {
    }
}