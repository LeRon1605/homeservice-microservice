using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Users;

public class UserDeactivatedException : ResourceInvalidOperationException
{
    public UserDeactivatedException() : base("user has an inactive status so you cannot log in", ErrorCodes.UserAlreadyDeActive)
    {
        
    }
}