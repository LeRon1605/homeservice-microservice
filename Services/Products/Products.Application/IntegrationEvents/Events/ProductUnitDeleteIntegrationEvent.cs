using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record ProductUnitDeleteIntegrationEvent : IntegrationEvent
{
    public Guid ProductUnitId { get; set; }

    public ProductUnitDeleteIntegrationEvent(Guid productUnitId)
    {
        ProductUnitId = productUnitId;
    }
}