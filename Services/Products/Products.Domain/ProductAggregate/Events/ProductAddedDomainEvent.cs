using BuildingBlocks.Domain.Event;
using MediatR;

namespace Products.Domain.ProductAggregate.Events;

public class ProductAddedDomainEvent : IDomainEvent
{
    public Product Product { get; }
    public ProductAddedDomainEvent(Product product)
    {
        Product = product;
    }
}