using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductUnitUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid ProductUnitId { get; set; }
    public string Name { get; set; }

    public ProductUnitUpdatedIntegrationEvent(Guid productUnitId, string name)
    {
        Name = name;
        ProductUnitId = productUnitId;
    }
}