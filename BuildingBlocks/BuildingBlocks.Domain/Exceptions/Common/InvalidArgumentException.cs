namespace BuildingBlocks.Domain.Exceptions.Common;

public class InvalidArgumentException : CoreException
{
    public InvalidArgumentException(string message) : base(message, ErrorCodes.InvalidArgument)
    {
    }
}