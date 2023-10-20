using BuildingBlocks.Application.IntegrationEvent;

namespace Installations.Application.IntegrationEvents.Events.Employees;

public record EmployeeUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid EmployeeId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public Guid RoleId { get; private set; }
    public string RoleName { get; private set; }

    public EmployeeUpdatedIntegrationEvent(Guid id, string fullName, string email, string phone, Guid roleId,
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