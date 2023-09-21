using BuildingBlocks.Application.IntegrationEvent;
using Customers.Application.Commands.AddSignedUpCustomer;
using Customers.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customers.Application.IntegrationEvents.Handlers;

public class UserSignedUpIntegrationEventHandler : IIntegrationEventHandler<UserSignedUpIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserSignedUpIntegrationEventHandler> _logger;

    public UserSignedUpIntegrationEventHandler(IMediator mediator,
                                             ILogger<UserSignedUpIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(UserSignedUpIntegrationEvent @event)
    {
        _logger.LogInformation($"User with id {@event.UserId} signed up and added to customer list");
        var command = new AddSignedUpCustomerCommand(@event.UserId, @event.FullName, @event.Email, @event.Phone);
        await _mediator.Send(command);
    }
}