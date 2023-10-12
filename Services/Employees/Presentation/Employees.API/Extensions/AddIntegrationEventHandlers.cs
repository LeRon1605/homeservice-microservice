using BuildingBlocks.Application.IntegrationEvent;
using Employees.Application.IntegrationEvents.Events.Role;
using Employees.Application.IntegrationEvents.Handlers;

namespace Employees.API.Extensions;

public static class IntegrationEventHandlerExtension
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<RoleCreatedIntegrationEvent>, RoleCreatedIntegrationEventHandler>();
        return services;
    }
}