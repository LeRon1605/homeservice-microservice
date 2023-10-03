using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events.ProductUnits;

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