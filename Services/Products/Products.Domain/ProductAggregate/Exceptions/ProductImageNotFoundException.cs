using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductImageNotFoundException : ResourceNotFoundException
{
    public ProductImageNotFoundException(Guid id) : base(nameof(ProductImage), id, ErrorCodes.ProductImageNotFound)
    {
    }
}