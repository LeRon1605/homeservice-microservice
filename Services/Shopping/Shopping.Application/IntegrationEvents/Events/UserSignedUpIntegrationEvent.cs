using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record UserSignedUpIntegrationEvent : IntegrationEvent
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; }

    public UserSignedUpIntegrationEvent(Guid id, 
                                      string fullName,
                                      string? email,
                                      string phone)
    {
        UserId = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
    }
}

