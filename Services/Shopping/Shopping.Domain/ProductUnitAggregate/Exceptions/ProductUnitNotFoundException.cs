using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.ProductUnitAggregate.Exceptions;

public class ProductUnitNotFoundException : ResourceNotFoundException
{
    public ProductUnitNotFoundException(Guid id) : base(nameof(ProductUnit), id, ErrorCodes.ProductUnitNotFound)
    {
    }
}