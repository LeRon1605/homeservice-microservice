using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Products.Domain.ProductAggregate;

public class ProductImage : Entity
{
    public string Url { get; private set; }
    public Guid ProductId { get; private set; }

    public ProductImage(string url, Guid productId)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(Url));
        ProductId = Guard.Against.Null(productId, nameof(ProductId));
    }
}