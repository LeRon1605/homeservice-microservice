using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductDeletedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }

    public ProductDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}