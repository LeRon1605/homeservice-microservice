using BuildingBlocks.Domain.Event;
using Products.Domain.ProductUnitAggregate;

namespace Products.Domain.ProductAggregate.Events;

public class ProductUnitAddedDomainEvent : IDomainEvent
{
    public ProductUnit ProductUnit { get; init; }

    public ProductUnitAddedDomainEvent(ProductUnit productUnit)
    {
        ProductUnit = productUnit;
    }
}