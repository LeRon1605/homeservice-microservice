using BuildingBlocks.Application.IntegrationEvent;

namespace Customers.Application.IntegrationEvents.Events;

public record CustomerDeletedIntegrationEvent : IntegrationEvent
{
    public Guid CustomerId { get; init; }

    public CustomerDeletedIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;
    }
}