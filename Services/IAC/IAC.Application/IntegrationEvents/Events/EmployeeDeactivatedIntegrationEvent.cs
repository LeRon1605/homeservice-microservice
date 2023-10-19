using BuildingBlocks.Application.IntegrationEvent;
using IAC.Domain.Enums;

namespace IAC.Application.IntegrationEvents.Events;

public record EmployeeDeactivatedIntegrationEvent : IntegrationEvent
{
    public Guid EmployeeId { get;set; }
    public Status Status { get; set; }

    public EmployeeDeactivatedIntegrationEvent(Guid employeeId, Status status)
    {
        EmployeeId = employeeId;
        Status = status;
    }
}