using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ProductAggregate.Exceptions;

public class ProductNotFoundException : ResourceNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id, ErrorCodes.ProductNotFound)
    {
    }
}