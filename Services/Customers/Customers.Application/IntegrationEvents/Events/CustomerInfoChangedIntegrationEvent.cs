using BuildingBlocks.Application.IntegrationEvent;

namespace Customers.Application.IntegrationEvents.Events;

public record CustomerInfoChangedIntegrationEvent : IntegrationEvent
{
    public Guid CustomerId { get; init; }
    public string ContactName { get; init; }
    
    public string? Email { get; init; }
    
    public string Phone { get; init; }

    public CustomerInfoChangedIntegrationEvent(Guid id, string contactName, string phone, string? email = null)
    {
        CustomerId = id;
        ContactName = contactName;
        Email = email;
        Phone = phone;
    }

}