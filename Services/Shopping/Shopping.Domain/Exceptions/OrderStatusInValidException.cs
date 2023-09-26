using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.Exceptions;

public class OrderStatusInValidException : ResourceInvalidOperationException
{
    public OrderStatusInValidException(string message) : base(message, ErrorCodes.OrderStatusInvalid)
    {
    }
}