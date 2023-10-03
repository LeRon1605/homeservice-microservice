using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events;

public record ProductUnitAddedIntegrationEvent : IntegrationEvent
{
    public Guid ProductUnitId { get; set; }
    public string Name { get; set; }

    public ProductUnitAddedIntegrationEvent(Guid productUnitId, string name)
    {
        Name = name;
        ProductUnitId = productUnitId;
    }
}