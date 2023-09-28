using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record ProductUnitDeletedIntegrationEvent : IntegrationEvent
{
    public Guid ProductUnitId { get; set; }

    public ProductUnitDeletedIntegrationEvent(Guid productUnitId)
    {
        ProductUnitId = productUnitId;
    }
}