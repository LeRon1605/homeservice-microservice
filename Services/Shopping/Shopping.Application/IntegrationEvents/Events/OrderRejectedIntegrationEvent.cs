using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record OrderRejectedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; private set; }

    public OrderRejectedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}