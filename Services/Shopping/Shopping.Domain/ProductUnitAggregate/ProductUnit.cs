using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.ProductUnitAggregate;

public class ProductUnit : AggregateRoot
{
    public string Name { get; set; }
    
    public ProductUnit(Guid id, string name)
    {
        Id = id;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
    }

    public void Update(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
    }
}