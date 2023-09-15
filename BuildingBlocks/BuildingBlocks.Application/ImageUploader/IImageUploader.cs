namespace BuildingBlocks.Application.ImageUploader;

public interface IImageUploader
{
    Task<UploaderResult> UploadAsync(string name, Stream stream);
}