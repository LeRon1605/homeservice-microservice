using BuildingBlocks.Application.IntegrationEvent;

namespace Employees.Application.IntegrationEvents.Events.Employees;

public record EmployeeAddedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public Guid RoleId { get; private set; }
    public string RoleName { get; set; }
    public string Password { get; set; }

    public EmployeeAddedIntegrationEvent(Guid id, string fullName, string email, string? phone, Guid roleId,
        string roleName, string password)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
        RoleId = roleId;
        RoleName = roleName;
        Password = password;
    }
}