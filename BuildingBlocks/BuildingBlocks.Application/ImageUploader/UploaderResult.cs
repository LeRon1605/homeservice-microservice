namespace BuildingBlocks.Application.ImageUploader;

public class UploaderResult
{
    public bool IsSuccess { get; private set; }
    public string? Url { get; private set; }
    public string? ErrorMessage { get; private set; }

    private UploaderResult(bool isSuccess, string? url, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Url = url;
        ErrorMessage = errorMessage;
    }

    public static UploaderResult Success(string url)
    {
        return new UploaderResult(true, url, null);
    }
    
    public static UploaderResult Failed(string errorMessage)
    {
        return new UploaderResult(false, null, errorMessage);
    }
}