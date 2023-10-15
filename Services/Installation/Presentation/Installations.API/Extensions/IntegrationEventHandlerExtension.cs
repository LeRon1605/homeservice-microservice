using BuildingBlocks.Application.IntegrationEvent;
using Installations.Application.IntegrationEvents.Events.Contracts;
using Installations.Application.IntegrationEvents.Handlers;

namespace Installations.API.Extensions;

public static class IntegrationEventHandlerExtension
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<ContractCreatedIntegrationEvent>, ContractCreatedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<ContractInstallationsUpdatedIntegrationEvent>, ContractInstallationsUpdatedIntegrationEventHandler>();
        return services;
    }
}