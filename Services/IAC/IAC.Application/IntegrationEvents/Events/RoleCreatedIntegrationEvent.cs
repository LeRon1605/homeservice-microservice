using BuildingBlocks.Application.IntegrationEvent;

namespace IAC.Application.IntegrationEvents.Events;

public record RoleCreatedIntegrationEvent: IntegrationEvent
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }

    public RoleCreatedIntegrationEvent(string roleId,string roleName)
    {
        RoleName = roleName;
        RoleId = roleId;
    }
}