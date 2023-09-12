namespace BuildingBlocks.Domain.Exceptions;

public class CoreException: Exception
{
    public string ErrorCode { get; set; }
    
    public CoreException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }
}