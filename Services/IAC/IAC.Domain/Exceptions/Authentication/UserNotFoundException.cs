using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Authentication;

public class UserNotFoundException : ResourceNotFoundException
{
    public UserNotFoundException(string id) : base("User", "phone/email", id, ErrorCodes.UserNotFound)
    {
    }
}