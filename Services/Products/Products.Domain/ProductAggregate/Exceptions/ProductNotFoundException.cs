using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductNotFoundException : ResourceNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id, ErrorCodes.ProductNotFound)
    {
    }
}