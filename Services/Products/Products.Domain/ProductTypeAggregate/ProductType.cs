using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Products.Domain.ProductTypeAggregate;

public class ProductType : AggregateRoot
{
    public string Name { get; private set; }

    public ProductType(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }
}