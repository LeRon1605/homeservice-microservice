using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.ImageUploader;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate.Exceptions;

namespace Products.Application.Commands.ProductCommands.UploadProductImage;

public class UploadProductImageCommandHandler : ICommandHandler<UploadProductImageCommand, ProductImageUploadResultDto>
{
    private readonly IImageUploader _imageUploader;
    
    public UploadProductImageCommandHandler(IImageUploader imageUploader)
    {
        _imageUploader = imageUploader;
    }
    
    public async Task<ProductImageUploadResultDto> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = await _imageUploader.UploadAsync(request.File.FileName, request.File.OpenReadStream());
        if (!result.IsSuccess)
        {
            throw new ProductImageUploadFailedException(request.File.FileName);
        }

        return new ProductImageUploadResultDto()
        {
            Url = result.Url!
        };
    }
}