using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.ProductAggregate.Events;

namespace Products.Application.DomainEventHandlers;

public class ProductUnitDeletedDomainEventHandler : IDomainEventHandler<ProductUnitDeletedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductUnitDeletedDomainEventHandler> _logger;

    public ProductUnitDeletedDomainEventHandler(IEventBus eventBus,
        ILogger<ProductUnitDeletedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task Handle(ProductUnitDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var productUnitDeletedIntegrationEvent = new ProductDeletedIntegrationEvent(notification.ProductUnit.Id);
        _eventBus.Publish(productUnitDeletedIntegrationEvent);
        _logger.LogInformation("Published delete product unit event with ProductId:{productUnitId}",
            notification.ProductUnit.Id);
        return Task.CompletedTask;
    }
}