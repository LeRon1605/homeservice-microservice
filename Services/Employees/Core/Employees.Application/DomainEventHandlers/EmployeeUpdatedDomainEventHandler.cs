using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Employees.Application.IntegrationEvents.Events.Employees;
using Employees.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Employees.Application.DomainEventHandlers;

public class EmployeeUpdatedDomainEventHandler : IDomainEventHandler<EmployeeUpdatedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<EmployeeUpdatedDomainEventHandler> _logger;

    public EmployeeUpdatedDomainEventHandler(IEventBus eventBus, ILogger<EmployeeUpdatedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task Handle(EmployeeUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var employeeUpdatedIntegrationEvent = new EmployeeUpdatedIntegrationEvent(notification.Employee.Id,
                notification.Employee.FullName,
                notification.Employee.Email,
                notification.Employee.Phone,
                notification.Employee.RoleId,
                notification.Employee.Role.Name);

        _eventBus.Publish(employeeUpdatedIntegrationEvent);

        _logger.LogInformation("Published integration event: {EventName}",
            employeeUpdatedIntegrationEvent.GetType().Name);
        return Task.CompletedTask;
    }
}