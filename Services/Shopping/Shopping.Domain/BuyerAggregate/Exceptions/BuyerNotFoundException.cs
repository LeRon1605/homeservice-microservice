using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.BuyerAggregate.Exceptions;

public class BuyerNotFoundException : ResourceNotFoundException
{
    public BuyerNotFoundException(Guid id) : base(nameof(Buyer), id, ErrorCodes.BuyerNotFound)
    {
    }
}