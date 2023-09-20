using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductTypeAggregate.Exceptions;

public class ProductTypeNotFoundException : ResourceNotFoundException
{
    public ProductTypeNotFoundException(Guid id) : base(nameof(ProductType), id, ErrorCodes.ProductTypeNotFound)
    {
    }
}