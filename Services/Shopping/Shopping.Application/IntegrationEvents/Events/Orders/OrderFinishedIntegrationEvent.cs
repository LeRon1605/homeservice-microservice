using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events.Orders;

public record OrderFinishedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    
    public OrderFinishedIntegrationEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}