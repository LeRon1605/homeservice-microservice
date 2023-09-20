using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductGroupAggregate.Exceptions;

public class ProductGroupNotFoundException : ResourceNotFoundException
{
    public ProductGroupNotFoundException(Guid id) : base(nameof(ProductGroup), id, ErrorCodes.ProductGroupNotFound)
    {
    }
}