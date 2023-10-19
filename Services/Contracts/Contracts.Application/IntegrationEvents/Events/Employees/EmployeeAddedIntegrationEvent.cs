using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events.Employees;

public record EmployeeAddedIntegrationEvent : IntegrationEvent
{
    public Guid EmployeeId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public Guid RoleId { get; private set; }
    public string RoleName { get; private set; }
    public string Password { get; private set; }

    public EmployeeAddedIntegrationEvent(
        Guid employeeId, 
        string fullName, 
        string email, 
        string? phone, 
        Guid roleId,
        string roleName, 
        string password)
    {
        EmployeeId = employeeId;
        FullName = fullName;
        Email = email;
        Phone = phone;
        RoleId = roleId;
        RoleName = roleName;
        Password = password;
    }
}