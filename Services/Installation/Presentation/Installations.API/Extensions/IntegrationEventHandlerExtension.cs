using BuildingBlocks.Application.IntegrationEvent;
using Installations.Application.IntegrationEvents.Events.Contracts;
using Installations.Application.IntegrationEvents.Events.Employees;
using Installations.Application.IntegrationEvents.Events.Materials;
using Installations.Application.IntegrationEvents.Handlers;
using Installations.Application.IntegrationEvents.Handlers.Employees;
using Installations.Application.IntegrationEvents.Handlers.Materials;

namespace Installations.API.Extensions;

public static class IntegrationEventHandlerExtension
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<ContractCreatedIntegrationEvent>, ContractCreatedIntegrationEventHandler>();
        // services.AddScoped<IIntegrationEventHandler<ContractInstallationsUpdatedIntegrationEvent>, ContractInstallationsUpdatedIntegrationEventHandler>();
        
        services.AddScoped<IIntegrationEventHandler<MaterialAddedIntegrationEvent>, MaterialAddedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<MaterialDeletedIntegrationEvent>, MaterialDeletedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<MaterialUpdatedIntegrationEvent>, MaterialUpdatedIntegrationEventHandler>();
        
        services.AddScoped<IIntegrationEventHandler<EmployeeAddedIntegrationEvent>, EmployeeAddedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<EmployeeDeactivatedIntegrationEvent>, EmployeeDeactivatedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<EmployeeUpdatedIntegrationEvent>, EmployeeUpdatedIntegrationEventHandler>();
        return services;
    }
}