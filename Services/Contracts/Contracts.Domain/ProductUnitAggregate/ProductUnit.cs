using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.ProductUnitAggregate;

public class ProductUnit : AuditableAggregateRoot
{
    public string Name { get; private set; }
    
    public ProductUnit(Guid id, string name)
    {
        Id = id;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
    }

    public void Update(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
    }

    private ProductUnit()
    {
        
    }
}