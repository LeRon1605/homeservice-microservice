using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using IAC.Application.IntegrationEvents.Events;
using IAC.Domain.Constants;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Roles;
using IAC.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAC.Application.IntegrationEvents.Handlers;

public class EmployeeAddedIntegrationEventHandler : IIntegrationEventHandler<EmployeeAddedIntegrationEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<EmployeeAddedIntegrationEventHandler> _logger;

    public EmployeeAddedIntegrationEventHandler(UserManager<ApplicationUser> userManager,
        ILogger<EmployeeAddedIntegrationEventHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Handle(EmployeeAddedIntegrationEvent @event)
    {
        _logger.LogInformation("Received EmployeeAddedIntegrationEvent for employee with id: {employeeId}", @event.Id);

        var user = new ApplicationUser
        {
            Id = @event.EmployeeId.ToString(),
            UserName = @event.Email,
            FullName = @event.FullName,
            Email = @event.Email,
            PhoneNumber = @event.Phone
        };

        var result = await _userManager.CreateAsync(user, @event.Password);

        if (!result.Succeeded)
            throw new UserCreateFailException(result.Errors.First().Description);
        var addRoleResult = await _userManager.AddToRoleAsync(user, @event.RoleName);
        if (!addRoleResult.Succeeded)
            throw new RoleNotFoundException(nameof(ApplicationRole.Name), @event.RoleName);

        _logger.LogInformation("User with id: {employeeId} was created in identity service", @event.Id);
    }
}