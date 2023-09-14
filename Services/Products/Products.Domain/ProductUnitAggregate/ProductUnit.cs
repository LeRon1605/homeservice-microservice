using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Products.Domain.ProductUnitAggregate;

public class ProductUnit : AggregateRoot
{
    public string Name { get; private set; }

    public ProductUnit(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }
}