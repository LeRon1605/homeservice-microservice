using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.PendingOrdersAggregate.Events;

public class OrderFinishedDomainEvent : IDomainEvent
{
    public PendingOrder Order { get; set; }
    
    public OrderFinishedDomainEvent(PendingOrder order)
    {
        Order = order;
    }
}