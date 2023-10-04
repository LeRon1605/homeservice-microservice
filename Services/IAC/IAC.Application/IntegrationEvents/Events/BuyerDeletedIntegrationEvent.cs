using BuildingBlocks.Application.IntegrationEvent;

namespace IAC.Application.IntegrationEvents.Events;

public record BuyerDeletedIntegrationEvent : IntegrationEvent
{
    public Guid CustomerId { get; init; }

    public BuyerDeletedIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;
    }
}