using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.Exceptions;

public class BuyerPhoneAlreadyExistException : ResourceAlreadyExistException
{
    public BuyerPhoneAlreadyExistException(string phone) : base($"Customer with phone {phone} already exist.", ErrorCodes.CustomerPhoneAlreadyExist)
    {
    } 
}