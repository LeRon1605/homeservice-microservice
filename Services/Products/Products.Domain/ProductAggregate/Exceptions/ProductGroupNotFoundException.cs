using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductGroupNotFoundException : ResourceNotFoundException
{
    public ProductGroupNotFoundException(Guid id) : base(nameof(Product), id, ErrorCodes.ProductGroupNotFound)
    {
    }
}