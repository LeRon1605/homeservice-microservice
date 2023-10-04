using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.CustomerAggregate.Events;

public class CustomerDeletedDomainEvent : IDomainEvent
{
    public Guid CustomerId { get; init; }
    
    public CustomerDeletedDomainEvent(Guid customerId)
    {
        CustomerId = customerId;
    }
}