using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.ProductAggregate.Exceptions;

public class ProductNotFoundException : ResourceNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id, ErrorCodes.ProductNotFound)
    {
    }
}