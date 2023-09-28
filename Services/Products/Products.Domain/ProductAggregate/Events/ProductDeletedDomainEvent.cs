using BuildingBlocks.Domain.Event;

namespace Products.Domain.ProductAggregate.Events;

public class ProductDeletedDomainEvent : IDomainEvent
{
    public Product Product { get; init; }

    public ProductDeletedDomainEvent(Product product)
    {
        Product = product;
    }
}