using BuildingBlocks.Domain.Exceptions.Common;

namespace IAC.Domain.Exceptions.Authentication;

public class InvalidPasswordException : InvalidInputException
{
    public InvalidPasswordException() : base("Invalid password", ErrorCodes.InvalidPassword)
    {
    }
}