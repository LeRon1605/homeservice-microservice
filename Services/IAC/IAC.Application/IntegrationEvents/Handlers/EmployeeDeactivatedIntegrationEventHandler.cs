using BuildingBlocks.Application.IntegrationEvent;
using IAC.Application.IntegrationEvents.Events;
using IAC.Domain.Entities;
using IAC.Domain.Enums;
using IAC.Domain.Exceptions.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAC.Application.IntegrationEvents.Handlers;

public class EmployeeDeactivatedIntegrationEventHandler : IIntegrationEventHandler<EmployeeDeactivatedIntegrationEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<EmployeeDeactivatedIntegrationEventHandler> _logger;

    public EmployeeDeactivatedIntegrationEventHandler(UserManager<ApplicationUser> userManager,
        ILogger<EmployeeDeactivatedIntegrationEventHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Handle(EmployeeDeactivatedIntegrationEvent @event)
    {
        _logger.LogInformation("Received EmployeeUpdatedIntegrationEvent for employee with id:  {@event.EmployeeId}",
            @event.EmployeeId);
        
        var user = await _userManager.FindByIdAsync(@event.EmployeeId.ToString());
        if (user == null)
            throw new UserNotFoundException(nameof(ApplicationUser.Id), @event.Id.ToString());
        user.Status = Status.Inactive;
        
        await _userManager.UpdateAsync(user);
        _logger.LogInformation("User with id: {employeeId} was updated status inactive in identity service", user.Id);
    }
}