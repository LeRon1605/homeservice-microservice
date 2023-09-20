using BuildingBlocks.Domain.Exceptions.Resource;

namespace Customers.Domain.Exceptions;

public class CustomerEmailAlreadyExistException : ResourceAlreadyExistException
{
    public CustomerEmailAlreadyExistException(string email) : base($"Customer with email {email} already exist.", ErrorCodes.CustomerEmailAlreadyExist)
    {
    }
}