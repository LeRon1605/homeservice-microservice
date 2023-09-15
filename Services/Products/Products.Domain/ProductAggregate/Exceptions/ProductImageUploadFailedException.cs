using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductImageUploadFailedException : ResourceInvalidOperationException
{
    public ProductImageUploadFailedException(string fileName) 
        : base($"Upload {fileName} failed!", ErrorCodes.ProductImageUploadFailed)
    {
    }
}