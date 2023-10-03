using BuildingBlocks.Application.IntegrationEvent;
using Shopping.Application.IntegrationEvents.EventHandling;
using Shopping.Application.IntegrationEvents.EventHandling.Orders;
using Shopping.Application.IntegrationEvents.EventHandling.Products;
using Shopping.Application.IntegrationEvents.EventHandling.ProductUnits;
using Shopping.Application.IntegrationEvents.EventHandling.Users;
using Shopping.Application.IntegrationEvents.Events;
using Shopping.Application.IntegrationEvents.Events.Orders;
using Shopping.Application.IntegrationEvents.Events.Products;
using Shopping.Application.IntegrationEvents.Events.ProductUnits;
using Shopping.Application.IntegrationEvents.Events.Users;

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
        
        services.AddScoped<IIntegrationEventHandler<UserSignedUpIntegrationEvent>, UserSignedUpIntegrationEventHandler>();

        services.AddScoped<IIntegrationEventHandler<OrderFinishedIntegrationEvent>, OrderFinishedIntegrationEventHandler>();
        return services;
    }
}