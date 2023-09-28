using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductUnitAddedIntegrationEvent : IntegrationEvent
{
    public Guid UnitId { get; set; }
    public string Name { get; private set; }

    public ProductUnitAddedIntegrationEvent(Guid unitId, string name)
    {
        UnitId = unitId;
        Name = name;
    }
}