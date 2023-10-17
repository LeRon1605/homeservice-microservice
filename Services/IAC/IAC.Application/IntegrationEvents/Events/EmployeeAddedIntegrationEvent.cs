using BuildingBlocks.Application.IntegrationEvent;

namespace IAC.Application.IntegrationEvents.Events;

public record EmployeeAddedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public string Password { get; set; }

    public EmployeeAddedIntegrationEvent(Guid id, string fullName, string email, string? phone, string roleId,
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