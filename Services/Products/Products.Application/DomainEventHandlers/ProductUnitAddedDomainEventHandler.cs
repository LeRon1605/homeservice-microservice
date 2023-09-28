using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.ProductAggregate.Events;

namespace Products.Application.DomainEventHandlers;

public class ProductUnitAddedDomainEventHandler : IDomainEventHandler<ProductUnitAddedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductUnitAddedDomainEventHandler> _logger;

    public ProductUnitAddedDomainEventHandler(IEventBus eventBus, ILogger<ProductUnitAddedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }
    public Task Handle(ProductUnitAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        var productUnitAddedIntegrationEvent = new ProductUnitAddedIntegrationEvent(notification.ProductUnit.Id,
            notification.ProductUnit.Name);
        _eventBus.Publish(productUnitAddedIntegrationEvent);
        _logger.LogInformation("Published add product unit event with ProductUnitId:{productUnitId}", notification.ProductUnit.Id);
        return Task.CompletedTask;
    }
}