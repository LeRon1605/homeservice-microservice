using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.ProductAggregate.Exceptions;

public class ProductImageDuplicateException : ResourceAlreadyExistException
{
    public ProductImageDuplicateException(string url) 
        : base(nameof(ProductImage), "Url", url, ErrorCodes.ProductImageDuplicate)
    {
    }
}