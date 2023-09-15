using BuildingBlocks.Application.ImageUploader;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.Cloudinary;

public class CloudinaryImageUploader : IImageUploader
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly ILogger<CloudinaryImageUploader> _logger;

    public CloudinaryImageUploader(
        CloudinaryDotNet.Cloudinary cloudinary,
        ILogger<CloudinaryImageUploader> logger)
    {
        _cloudinary = cloudinary;
        _logger = logger;
    }
    
    public async Task<UploaderResult> UploadAsync(string name, Stream stream)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription($"{Guid.NewGuid()}-{name}", stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = false,
            Folder = "HomeService"
        };

        try
        {
            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error == null)
            {
                return UploaderResult.Success(result.Url.ToString());
            }
    
            _logger.LogError("Upload file {name} to cloudinary failed: {error}", name, result.Error.Message);
            return UploaderResult.Failed(result.Error.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("Upload file {name} to cloudinary failed: {error}", name, e.Message);
            return UploaderResult.Failed("Could not connect to server!");
        }
    }
}