using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Employees.Application.IntegrationEvents.Events.Employees;
using Employees.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Employees.Application.DomainEventHandlers;

public class EmployeeDeactivatedDomainEventHandler : IDomainEventHandler<EmployeeDeactivatedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<EmployeeDeactivatedDomainEventHandler> _logger;

    public EmployeeDeactivatedDomainEventHandler(IEventBus eventBus,
        ILogger<EmployeeDeactivatedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task Handle(EmployeeDeactivatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var employeeDeactivatedIntegrationEvent =
            new EmployeeDeactivatedIntegrationEvent(notification.Employee.Id,
                notification.Employee.Status);

        _eventBus.Publish(employeeDeactivatedIntegrationEvent);

        _logger.LogInformation("Published integration event: {EventName}",
            employeeDeactivatedIntegrationEvent.GetType().Name);
        return Task.CompletedTask;
    }
}