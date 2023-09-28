using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductUnitUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid UnitId { get; set; }
    public string Name { get; private set; }

    public ProductUnitUpdatedIntegrationEvent(Guid unitId, string name)
    {
        UnitId = unitId;
        Name = name;
    }
}