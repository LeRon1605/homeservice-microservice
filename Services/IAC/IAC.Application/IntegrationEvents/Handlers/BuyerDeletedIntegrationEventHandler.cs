using BuildingBlocks.Application.IntegrationEvent;
using IAC.Application.IntegrationEvents.Events;
using IAC.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace IAC.Application.IntegrationEvents.Handlers;

public class BuyerDeletedIntegrationEventHandler : IIntegrationEventHandler<BuyerDeletedIntegrationEvent>
{
    private readonly IUserService _userService;
    private readonly ILogger<BuyerDeletedIntegrationEventHandler> _logger;

    public BuyerDeletedIntegrationEventHandler(IUserService userService,
                                                  ILogger<BuyerDeletedIntegrationEventHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task Handle(BuyerDeletedIntegrationEvent @event)
    {
        _logger.LogInformation("Received CustomerDeletedIntegrationEvent for customer with id: {customerId}", @event.CustomerId);
        
        if (!await _userService.AnyAsync(@event.CustomerId))
            return;
        
        _logger.LogInformation("Deleting user with id: {customerId} from identity service", @event.CustomerId);

        await _userService.DeleteUserAsync(@event.CustomerId);

        _logger.LogInformation("Deleted user with id: {customerId}", @event.CustomerId);
    }
}