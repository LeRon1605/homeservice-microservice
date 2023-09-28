using BuildingBlocks.Application.IntegrationEvent;
using Shopping.Application.IntegrationEvents.EventHandling;
using Shopping.Application.IntegrationEvents.Events;

namespace Shopping.API.Extensions;

public static class IntegrationEventHandlerExtension
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<ProductAddedIntegrationEvent>, ProductAddedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<ProductUpdatedIntegrationEvent>, ProductUpdatedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<ProductDeletedIntegrationEvent>, ProductDeletedIntegrationEventHandler>();
        
        services.AddScoped<IIntegrationEventHandler<ProductUnitAddedIntegrationEvent>, ProductUnitAddedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<ProductUnitUpdatedIntegrationEvent>, ProductUnitUpdatedIntegrationEventHandler>();
        services.AddScoped<IIntegrationEventHandler<ProductUnitDeletedIntegrationEvent>, ProductUnitDeletedIntegrationEventHandler>();

        return services;
    }
}