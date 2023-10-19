using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.Employees.UpdateEmployee;
using Contracts.Application.IntegrationEvents.Events.Employees;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling.Employees;

public class EmployeeUpdatedIntegrationEventHandler : IIntegrationEventHandler<EmployeeUpdatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmployeeUpdatedIntegrationEventHandler> _logger;
    
    public EmployeeUpdatedIntegrationEventHandler(
        IMediator mediator,
        ILogger<EmployeeUpdatedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(EmployeeUpdatedIntegrationEvent @event)
    {
        _logger.LogInformation("Received EmployeeUpdatedIntegrationEvent for employee with id: {employeeId}", @event.EmployeeId);
        
        var command = new UpdateEmployeeCommand(@event.EmployeeId, @event.FullName, @event.RoleName);
        await _mediator.Send(command);
    }
}