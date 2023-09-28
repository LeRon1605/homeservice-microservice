using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.Exceptions;

public class BuyerEmailAlreadyExistException : ResourceAlreadyExistException
{
    public BuyerEmailAlreadyExistException(string email) : base($"Customer with email {email} already exist.", ErrorCodes.CustomerEmailAlreadyExist)
    {
    }
}