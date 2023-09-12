namespace BuildingBlocks.Domain.Exceptions.Resource;

public class ResourceAccessDeniedException : CoreException
{
    public ResourceAccessDeniedException(string message) : base(message, ErrorCodes.ResourceAccessDenied)
    {
    }

    public ResourceAccessDeniedException(string message, string errorCode) : base(message, errorCode)
    {
    }
}