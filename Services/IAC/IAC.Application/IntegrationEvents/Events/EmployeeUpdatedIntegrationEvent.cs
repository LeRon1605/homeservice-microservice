using BuildingBlocks.Application.IntegrationEvent;

namespace IAC.Application.IntegrationEvents.Events;

public record EmployeeUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid EmployeeId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public string RoleId { get; private set; }
    public string RoleName { get; private set; }

    public EmployeeUpdatedIntegrationEvent(Guid id, string fullName, string email, string phone, string roleId,
        string roleName)
    {
        EmployeeId = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
        RoleId = roleId;
        RoleName = roleName;
    }
}