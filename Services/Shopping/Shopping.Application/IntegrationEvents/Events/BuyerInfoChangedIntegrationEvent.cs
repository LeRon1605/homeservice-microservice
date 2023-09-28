using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record BuyerInfoChangedIntegrationEvent : IntegrationEvent
{
    public Guid CustomerId { get; init; }
    public string FullName { get; init; }
    
    public string? Email { get; init; }
    
    public string Phone { get; init; }

    public BuyerInfoChangedIntegrationEvent(Guid id, string fullName, string phone, string? email = null)
    {
        CustomerId = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
    }

}