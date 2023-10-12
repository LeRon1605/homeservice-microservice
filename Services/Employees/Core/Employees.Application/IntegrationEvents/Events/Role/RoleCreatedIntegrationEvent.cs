using BuildingBlocks.Application.IntegrationEvent;

namespace Employees.Application.IntegrationEvents.Events.Role;

public record RoleCreatedIntegrationEvent : IntegrationEvent
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }

    public RoleCreatedIntegrationEvent(string roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}