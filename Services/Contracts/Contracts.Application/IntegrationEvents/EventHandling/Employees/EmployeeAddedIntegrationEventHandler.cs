using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.Employees;
using Contracts.Application.Commands.Employees.AddEmployee;
using Contracts.Application.IntegrationEvents.Events.Employees;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling.Employees;

public class EmployeeAddedIntegrationEventHandler : IIntegrationEventHandler<EmployeeAddedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmployeeAddedIntegrationEventHandler> _logger;
    
    public EmployeeAddedIntegrationEventHandler(
        IMediator mediator,
        ILogger<EmployeeAddedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(EmployeeAddedIntegrationEvent @event)
    {
        _logger.LogInformation("Received EmployeeAddedIntegrationEvent for employee with id: {employeeId}", @event.EmployeeId);
        
        var command = new AddEmployeeCommand(
            @event.EmployeeId,
            @event.FullName,
            @event.RoleName);
        await _mediator.Send(command);
    }
}