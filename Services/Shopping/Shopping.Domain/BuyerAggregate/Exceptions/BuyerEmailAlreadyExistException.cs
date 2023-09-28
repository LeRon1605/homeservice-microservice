
using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.BuyerAggregate.Exceptions;

public class BuyerEmailAlreadyExistException : ResourceAlreadyExistException
{
    public BuyerEmailAlreadyExistException(string email) : base($"Customer with email {email} already exist.", ErrorCodes.BuyerEmailAlreadyExist)
    {
    }
}