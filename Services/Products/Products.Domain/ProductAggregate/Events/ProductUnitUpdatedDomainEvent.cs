using BuildingBlocks.Domain.Event;
using Products.Domain.ProductUnitAggregate;

namespace Products.Domain.ProductAggregate.Events;

public class ProductUnitUpdatedDomainEvent : IDomainEvent
{
    public ProductUnit ProductUnit { get; init; }

    public ProductUnitUpdatedDomainEvent(ProductUnit productUnit)
    {
        ProductUnit = productUnit;
    }
}