using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductUnitUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid UnitId { get; private set; }
    public string Name { get; private set; }
}