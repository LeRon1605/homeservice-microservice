using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Domain.ProductAggregate;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    
    public decimal Price { get; private set; }
    
    public Guid ProductGroupId { get; private set; }
    
    public Guid? ProductUnitId { get; set; }
    
    public List<ProductReview> Reviews { get; private set; }
    
    public ICollection<OrderLine> OrderLines { get; set; }

    public Product(Guid id, string name, Guid productGroupId,Guid? productUnitId, decimal price)
    {
        Id = Guard.Against.Null(id, nameof(Id));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ProductGroupId = Guard.Against.Null(productGroupId, nameof(ProductGroupId));
        ProductUnitId = productUnitId;
        Price = price;
        
        Reviews = new List<ProductReview>();
    }

    public void AddReview(string description, int rating)
    {
        Reviews.Add(new ProductReview(description, rating, Id));
    }

    public void Update(string name, Guid productGroupId,Guid? productUnitId, decimal sellPrice)
    {
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        Price = sellPrice;
    }
}