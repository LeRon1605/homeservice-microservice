using BuildingBlocks.Domain.Exceptions.Resource;

namespace Customers.Domain.Exceptions;

public class CustomerPhoneAlreadyExistException : ResourceAlreadyExistException
{
    public CustomerPhoneAlreadyExistException(string phone) : base($"Customer with phone {phone} already exist.", ErrorCodes.CustomerPhoneAlreadyExist)
    {
    } 
}