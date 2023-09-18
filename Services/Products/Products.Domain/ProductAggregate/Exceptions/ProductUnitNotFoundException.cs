using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductUnitNotFoundException : ResourceNotFoundException
{
    public ProductUnitNotFoundException(Guid id) : base(nameof(Product), id, ErrorCodes.ProductTypeNotFound)
    {
    }
}