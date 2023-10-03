using BuildingBlocks.Domain.Event;

namespace Shopping.Domain.OrderAggregate.Events;

public class OrderAddedDomainEvent : IDomainEvent
{
    public Order Order { get; set; }
    
    public OrderAddedDomainEvent(Order order)
    {
        Order = order;
    }
}