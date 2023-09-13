using BuildingBlocks.Domain.Models;

namespace Products.Domain.ProductAggregate;

public class Product : AggregateRoot
{
    public string? Name { get; private set; } 
    
    public Product(string? name)
    {
        Name = name;
    }
}