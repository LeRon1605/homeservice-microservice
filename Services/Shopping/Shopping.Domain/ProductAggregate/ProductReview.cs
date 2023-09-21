using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.ProductAggregate;

public class ProductReview : Entity
{
    public string Description { get; private set; }
    
    public int Rating { get; private set; }
    
    public Guid ProductId { get; private set; }
    
    public ProductReview(string description, int rating, Guid productId)
    {
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(Description));
        Rating = Guard.Against.NegativeOrZero(rating, nameof(Rating));
        ProductId = productId;
    }
}