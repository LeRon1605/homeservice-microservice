using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class DuplicateProductCodeException : ResourceAlreadyExistException
{
    public DuplicateProductCodeException(string message) : base(message, ErrorCodes.ProductCodeDuplicate)
    {
    }
}