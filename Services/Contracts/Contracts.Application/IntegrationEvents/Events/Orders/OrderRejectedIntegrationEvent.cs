using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events.Orders;

public record OrderRejectedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; private set; }

    public OrderRejectedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}