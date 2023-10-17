using BuildingBlocks.Application.IntegrationEvent;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.IntegrationEvents.Events.Employees;

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