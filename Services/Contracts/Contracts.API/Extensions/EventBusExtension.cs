using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using Contracts.Application.IntegrationEvents.Events;
using Contracts.Application.IntegrationEvents.Events.Orders;
using Contracts.Application.IntegrationEvents.Events.Products;
using Contracts.Application.IntegrationEvents.Events.ProductUnits;

namespace Contracts.API.Extensions;

public static class EventBusExtension
{
    public static void UseEventBus(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        eventBus.Subscribe<ProductAddedIntegrationEvent, IIntegrationEventHandler<ProductAddedIntegrationEvent>>();
        eventBus.Subscribe<ProductUpdatedIntegrationEvent, IIntegrationEventHandler<ProductUpdatedIntegrationEvent>>();
        eventBus.Subscribe<ProductDeletedIntegrationEvent, IIntegrationEventHandler<ProductDeletedIntegrationEvent>>();
        
        eventBus.Subscribe<ProductUnitAddedIntegrationEvent, IIntegrationEventHandler<ProductUnitAddedIntegrationEvent>>();
        eventBus.Subscribe<ProductUnitUpdatedIntegrationEvent, IIntegrationEventHandler<ProductUnitUpdatedIntegrationEvent>>();
        eventBus.Subscribe<ProductUnitDeletedIntegrationEvent, IIntegrationEventHandler<ProductUnitDeletedIntegrationEvent>>();
        
        eventBus.Subscribe<OrderAddedIntegrationEvent, IIntegrationEventHandler<OrderAddedIntegrationEvent>>();
        
        eventBus.Subscribe<OrderRejectedIntegrationEvent, IIntegrationEventHandler<OrderRejectedIntegrationEvent>>();
    }
}