using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.ProductAggregate.Events;

namespace Products.Application.DomainEventHandlers;

public class ProductUnitUpdatedDomainEventHandler : IDomainEventHandler<ProductUnitUpdatedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductUnitUpdatedDomainEventHandler> _logger;

    public ProductUnitUpdatedDomainEventHandler(IEventBus eventBus, ILogger<ProductUnitUpdatedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task Handle(ProductUnitUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var productUnitUpdatedIntegrationEvent = new ProductUnitUpdatedIntegrationEvent(notification.ProductUnit.Id,
            notification.ProductUnit.Name);
        _eventBus.Publish(productUnitUpdatedIntegrationEvent);
        _logger.LogInformation("Published update product unit event with ProductUnitId:{productUnitId}",
            notification.ProductUnit.Id);
        return Task.CompletedTask;
    }
}