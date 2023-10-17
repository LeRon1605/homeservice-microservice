using BuildingBlocks.Application.IntegrationEvent;

namespace IAC.Application.IntegrationEvents.Events;

public record RoleCreatedIntegrationEvent: IntegrationEvent
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }

    public RoleCreatedIntegrationEvent(Guid roleId,string roleName)
    {
        RoleName = roleName;
        RoleId = roleId;
    }
}