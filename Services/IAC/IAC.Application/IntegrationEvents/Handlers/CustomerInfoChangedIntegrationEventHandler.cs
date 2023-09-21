using BuildingBlocks.Application.IntegrationEvent;
using IAC.Application.Dtos.Users;
using IAC.Application.IntegrationEvents.Events;
using IAC.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace IAC.Application.IntegrationEvents.Handlers;

public class CustomerInfoChangedIntegrationEventHandler : IIntegrationEventHandler<CustomerInfoChangedIntegrationEvent>
{
    private readonly IUserService _userService;
    private readonly ILogger<CustomerInfoChangedIntegrationEventHandler> _logger;

    public CustomerInfoChangedIntegrationEventHandler(ILogger<CustomerInfoChangedIntegrationEventHandler> logger,
                                                      IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task Handle(CustomerInfoChangedIntegrationEvent @event)
    {
        _logger.LogInformation("Received CustomerInfoChangedIntegrationEvent for customer with id: {customerId}", @event.CustomerId);
        
        if (!await _userService.AnyAsync(@event.CustomerId))
            return;
        
        _logger.LogInformation("Updating user with id: {customerId} in identity service", @event.CustomerId);

        var userInfoDto = new UserInfoDto
        {
            Id = @event.CustomerId,
            FullName = @event.ContactName,
            Phone = @event.Phone,
            Email = @event.Email
        };
        
        await _userService.UpdateUserInfoAsync(userInfoDto); 
        
        _logger.LogInformation("User with id: {customerId} was updated in identity service", @event.CustomerId);
    }
}
