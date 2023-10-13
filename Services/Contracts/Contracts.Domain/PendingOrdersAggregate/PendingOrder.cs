using BuildingBlocks.Domain.Models;
using Contracts.Domain.PendingOrdersAggregate.Events;

namespace Contracts.Domain.PendingOrdersAggregate;

public class PendingOrder : AuditableAggregateRoot
{
    public Guid BuyerId { get; private set; }
    public OrderContactInfo ContactInfo { get; private set; }

    public PendingOrder(
        Guid id,
        Guid buyerId,
        string customerName,
        string contactName, 
        string? email, 
        string phone, 
        string? address, 
        string? city, 
        string? state, 
        string? postalCode)
    {
        Id = id;
        BuyerId = buyerId;
        ContactInfo = new OrderContactInfo(customerName, contactName, email, phone, address, city, state, postalCode);
    }

    public void Finish()
    {
        AddDomainEvent(new OrderFinishedDomainEvent(this));
    }

    private PendingOrder()
    {
        
    }

    public void RejectOrder()
    {
        
    }
}