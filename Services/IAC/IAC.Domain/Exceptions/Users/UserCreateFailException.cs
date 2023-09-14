using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Users;

public class UserCreateFailException : ResourceCreateFailException
{
    public UserCreateFailException(string message) : base(message, ErrorCodes.UserCreateFail)
    {
    }
}