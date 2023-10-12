using BuildingBlocks.Application.IntegrationEvent;
using Employees.Application.Command.Role;
using Employees.Application.IntegrationEvents.Events.Role;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Employees.Application.IntegrationEvents.Handlers;

public class RoleCreatedIntegrationEventHandler : IIntegrationEventHandler<RoleCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoleCreatedIntegrationEventHandler> _logger;

    public RoleCreatedIntegrationEventHandler(IMediator mediator, ILogger<RoleCreatedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(RoleCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("Role name {@event.RoleName} created and added to employee ", @event.RoleName);
        var command = new RoleCreateCommand(@event.RoleId,@event.RoleName);
        await _mediator.Send(command);
    }
}