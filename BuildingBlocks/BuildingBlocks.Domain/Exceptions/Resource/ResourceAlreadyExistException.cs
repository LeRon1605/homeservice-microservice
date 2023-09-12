namespace BuildingBlocks.Domain.Exceptions.Resource;

public class ResourceAlreadyExistException : CoreException
{
    public ResourceAlreadyExistException(string message) : base(message, ErrorCodes.ResourceAlreadyExist)
    {
    }

    public ResourceAlreadyExistException(string message, string errorCode) : base(message, errorCode)
    {
    }

    public ResourceAlreadyExistException(string entityName, string column, string value) : base($"{entityName} with {column} '{value}' has already existed!", ErrorCodes.ResourceAlreadyExist)
    {
    }

    public ResourceAlreadyExistException(string entityName, string column, string value, string errorCode) : base($"{entityName} with {column} '{value}' has already existed!", errorCode)
    {
    }
}