using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Contracts.Domain.ProductUnitAggregate;

namespace Contracts.Domain.ProductAggregate;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Guid? UnitId { get; private set; }
    public ProductUnit? Unit { get; private set; }
    public List<string> Colors { get; private set; }
    
    public Product(
        Guid id,
        string name,
        Guid? unitId,
        decimal price,
        List<string>? colors = null)
    {
        Id = id;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        UnitId = unitId;
        Price = Guard.Against.Negative(price, nameof(Price));
        Colors = colors ?? new List<string>();
    }

    public void Update(string name, Guid? unitId, decimal price, List<string>? colors)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        UnitId = unitId;
        Price = Guard.Against.Negative(price, nameof(Price));
        Colors = colors ?? new List<string>();
    }

    private Product()
    {
        
    }
}