using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Products.Domain.ProductGroupAggregate;

public class ProductGroup : AuditableAggregateRoot
{
    public string Name { get; private set; }

    public ProductGroup(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }
}