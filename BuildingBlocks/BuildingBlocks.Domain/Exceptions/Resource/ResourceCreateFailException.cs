namespace BuildingBlocks.Domain.Exceptions.Resource;

public class ResourceCreateFailException : CoreException
{
    public ResourceCreateFailException(string message) : base(message, ErrorCodes.ResourceCreateFail)
    {
    }
    
    public ResourceCreateFailException(string message, string errorCode) : base(message, errorCode)
    {
    }
    
}