using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Authentication;

public class UserNotFoundException : ResourceNotFoundException
{
    public UserNotFoundException(string phoneOrEmail) : base("User", "phone/email", phoneOrEmail, ErrorCodes.UserNotFound)
    {
    }
    
    public UserNotFoundException(string column, string value) : base("User", "Id", value, ErrorCodes.UserNotFound)
    {
    }
    
    public UserNotFoundException(Guid userId) : base($"User with id {userId} was not found", ErrorCodes.UserNotFound)
    {
    }
}