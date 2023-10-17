using BuildingBlocks.Application.IntegrationEvent;

namespace Employees.Application.IntegrationEvents.Events.Role;

public record RoleCreatedIntegrationEvent : IntegrationEvent
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }

    public RoleCreatedIntegrationEvent(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}