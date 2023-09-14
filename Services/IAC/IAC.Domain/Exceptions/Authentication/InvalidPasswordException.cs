using BuildingBlocks.Domain.Exceptions;

namespace IAC.Domain.Exceptions.Authentication;

public class InvalidPasswordException : CoreException
{
    public InvalidPasswordException() : base("Invalid password", ErrorCodes.InvalidPassword)
    {
    }
}