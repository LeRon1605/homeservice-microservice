using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.IntegrationEvents.EventHandling;
using Contracts.Application.IntegrationEvents.EventHandling.Orders;
using Contracts.Application.IntegrationEvents.EventHandling.Products;
using Contracts.Application.IntegrationEvents.EventHandling.ProductUnits;
using Contracts.Application.IntegrationEvents.Events;
using Contracts.Application.IntegrationEvents.Events.Orders;
using Contracts.Application.IntegrationEvents.Events.Products;
using Contracts.Application.IntegrationEvents.Events.ProductUnits;

namespace Contracts.API.Extensions;

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

        services.AddScoped<IIntegrationEventHandler<OrderAddedIntegrationEvent>, OrderAddedIntegrationEventHandler>();

        services.AddScoped<IIntegrationEventHandler<OrderRejectedIntegrationEvent>, OrderRejectedIntegrationEventHandler>();
        return services;
    }
}