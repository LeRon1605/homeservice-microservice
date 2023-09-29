using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Shopping.Application.Commands.Buyers.AddSignedUpUser;
using Shopping.Application.IntegrationEvents.Events;

namespace Shopping.Application.IntegrationEvents.EventHandling;

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
        _logger.LogInformation($"User with id {@event.UserId} signed up and added to buyer list");
        var command = new AddSignedUpUserCommand(@event.UserId, @event.FullName, @event.Email, @event.Phone);
        await _mediator.Send(command);
    }
}