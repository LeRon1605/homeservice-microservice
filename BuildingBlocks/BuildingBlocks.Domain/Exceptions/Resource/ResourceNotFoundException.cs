namespace BuildingBlocks.Domain.Exceptions.Resource;

public class ResourceNotFoundException : CoreException
{
    public ResourceNotFoundException(string message) : base(message, ErrorCodes.ResourceNotFound)
    {
    }
    
    public ResourceNotFoundException(string message, string code) : base(message, code)
    {
    }
    
    public ResourceNotFoundException(string entityName, Guid id) : base($"{entityName} with id '{id}' does not exist!", ErrorCodes.ResourceNotFound)
    {
    }
    
    public ResourceNotFoundException(string entityName, Guid id, string code) : base($"{entityName} with id '{id}' does not exist!", code)
    {
    }

    public ResourceNotFoundException(string entityName, string column, string value) : base($"{entityName} with {column} '{value}' does not exist!", ErrorCodes.ResourceNotFound)
    {
    }
    
    public ResourceNotFoundException(string entityName, string column, string value, string code) : base($"{entityName} with {column} '{value}' does not exist!", code)
    {
    }
}