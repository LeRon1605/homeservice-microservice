using BuildingBlocks.Application.CQRS;
using Microsoft.AspNetCore.Http;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Commands.ProductCommands.UploadProductImage;

public class UploadProductImageCommand : ICommand<ProductImageUploadResultDto>
{
    public IFormFile File { get; set; }

    public UploadProductImageCommand(IFormFile file)
    {
        File = file;
    }
}