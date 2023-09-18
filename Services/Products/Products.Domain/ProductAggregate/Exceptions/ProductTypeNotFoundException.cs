using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductTypeNotFoundException : ResourceNotFoundException
{
    public ProductTypeNotFoundException(Guid id) : base(nameof(Product), id, ErrorCodes.ProductNotFound)
    {
    }
}