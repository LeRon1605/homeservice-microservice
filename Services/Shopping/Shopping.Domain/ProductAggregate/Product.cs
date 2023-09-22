using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.ProductAggregate;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    
    public decimal? Price { get; private set; }
    
    public Guid ProductTypeId { get; private set; }
    
    public List<ProductReview> Reviews { get; private set; }

    public Product(Guid id, string name, Guid productTypeId, decimal? price)
    {
        Id = Guard.Against.Null(id, nameof(Id));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ProductTypeId = Guard.Against.Null(productTypeId, nameof(ProductTypeId));
        Price = price;
        
        Reviews = new List<ProductReview>();
    }

    public void AddReview(string description, int rating)
    {
        Reviews.Add(new ProductReview(description, rating, Id));
    }

    public void Update(string name, Guid productTypeId, decimal? sellPrice)
    {
        Name = name;
        ProductTypeId = productTypeId;
        Price = sellPrice;
    }
}