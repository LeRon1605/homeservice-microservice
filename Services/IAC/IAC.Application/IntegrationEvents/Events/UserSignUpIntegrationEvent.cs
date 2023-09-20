using BuildingBlocks.Application.IntegrationEvent;

namespace IAC.Application.IntegrationEvents.Events;

public record UserSignUpIntegrationEvent : IntegrationEvent
{
    public string FullName { get; set; }

    public UserSignUpIntegrationEvent(string fullName)
    {
        FullName = fullName;
    }
}