using BuildingBlocks.Domain.Event;

namespace Products.Domain.ProductAggregate.Events;

public class ProductUpdatedDomainEvent: IDomainEvent
{
    public Product Product { get; }
    public ProductUpdatedDomainEvent(Product product)
    {
        Product = product;
    }
}