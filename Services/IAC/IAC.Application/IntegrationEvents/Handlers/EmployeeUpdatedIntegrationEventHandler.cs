using BuildingBlocks.Application.IntegrationEvent;
using IAC.Application.IntegrationEvents.Events;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using IAC.Domain.Exceptions.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAC.Application.IntegrationEvents.Handlers;

public class EmployeeUpdatedIntegrationEventHandler : IIntegrationEventHandler<EmployeeUpdatedIntegrationEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<EmployeeUpdatedIntegrationEventHandler> _logger;

    public EmployeeUpdatedIntegrationEventHandler(UserManager<ApplicationUser> userManager,
        ILogger<EmployeeUpdatedIntegrationEventHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Handle(EmployeeUpdatedIntegrationEvent @event)
    {
        _logger.LogInformation("Received EmployeeUpdatedIntegrationEvent for employee with id: {employeeId}",
            @event.Id);

        var user = await _userManager.FindByIdAsync(@event.Id.ToString());
        if (user == null)
            throw new UserNotFoundException(nameof(ApplicationUser.Id), @event.Id.ToString());
        user.FullName = @event.FullName;
        user.Email = @event.Email;
        user.PhoneNumber = @event.Phone;
        user.UserName = @event.Email;

        await _userManager.UpdateAsync(user);

        await _userManager.RemoveFromRoleAsync(user, @event.RoleName);
        
        var addToRoleResult = await _userManager.AddToRoleAsync(user, @event.RoleName);
        if (!addToRoleResult.Succeeded)
            throw new RoleNotFoundException(nameof(ApplicationRole.Name), @event.RoleName);
        
        _logger.LogInformation("User with id: {employeeId} was updated in identity service", user.Id);
    }
}