using BuildingBlocks.Domain.Event;

namespace Products.Domain.ProductAggregate.Events;

public class ProductDeletedDomainEvent : IDomainEvent
{
    public Product Product { get; }

    public ProductDeletedDomainEvent(Product product)
    {
        Product = product;
    }
}