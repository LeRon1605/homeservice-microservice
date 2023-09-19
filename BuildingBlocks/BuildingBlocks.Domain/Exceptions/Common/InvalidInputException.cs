namespace BuildingBlocks.Domain.Exceptions.Common;

public class InvalidInputException : CoreException
{
    public InvalidInputException(string message) : base(message, ErrorCodes.InvalidInput)
    {
    }
    
    public InvalidInputException(string message, string code) : base(message, code)
    {
    }
}