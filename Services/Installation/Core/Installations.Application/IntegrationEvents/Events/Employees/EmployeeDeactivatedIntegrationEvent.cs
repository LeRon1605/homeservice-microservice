using BuildingBlocks.Application.IntegrationEvent;
using Installations.Domain.InstallerAggregate.Enums;

namespace Installations.Application.IntegrationEvents.Events.Employees;

public record EmployeeDeactivatedIntegrationEvent : IntegrationEvent
{
    public Guid EmployeeId { get;set; }
    public Status Status { get; set; }

    public EmployeeDeactivatedIntegrationEvent(Guid id, Status status)
    {
        EmployeeId = id;
        Status = status;
    }
}