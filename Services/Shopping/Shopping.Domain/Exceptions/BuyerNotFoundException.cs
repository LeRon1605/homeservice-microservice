using BuildingBlocks.Domain.Exceptions.Resource;
using Shopping.Domain.BuyerAggregate;

namespace Shopping.Domain.Exceptions;

public class BuyerNotFoundException : ResourceNotFoundException
{
    public BuyerNotFoundException(Guid id) : base(nameof(Buyer), id, ErrorCodes.CustomerNotFound)
    {
    }
}