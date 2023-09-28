using BuildingBlocks.Domain.Exceptions.Resource;

namespace Shopping.Domain.BuyerAggregate.Exceptions;

public class BuyerPhoneAlreadyExistException : ResourceAlreadyExistException
{
    public BuyerPhoneAlreadyExistException(string phone) : base($"Customer with phone {phone} already exist.", ErrorCodes.BuyerPhoneAlreadyExist)
    {
    } 
}