using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Users;

public class UserExistException : ResourceAlreadyExistException
{
    public UserExistException(string message) : base(message, ErrorCodes.UserAlreadyExist)
    {
    }
}