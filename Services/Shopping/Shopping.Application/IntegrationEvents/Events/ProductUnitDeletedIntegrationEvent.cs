using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductUnitDeletedIntegrationEvent : IntegrationEvent
{
    public Guid ProductUnitId { get; set; }

    public ProductUnitDeletedIntegrationEvent(Guid productUnitId)
    {
        ProductUnitId = productUnitId;
    }
}