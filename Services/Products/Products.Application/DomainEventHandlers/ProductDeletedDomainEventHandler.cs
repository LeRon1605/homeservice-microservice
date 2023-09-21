using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.ProductAggregate.Events;

namespace Products.Application.DomainEventHandlers;

public class ProductDeletedDomainEventHandler : IDomainEventHandler<ProductDeletedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductAddedDomainEvent> _logger;

    public ProductDeletedDomainEventHandler(IEventBus eventBus, ILogger<ProductAddedDomainEvent> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }
    public Task Handle(ProductDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var productDeletedIntegrationEvent = new ProductDeletedIntegrationEvent(
            domainEvent.Product.Id,
            domainEvent.Product.Name,
            domainEvent.Product.ProductTypeId,
            domainEvent.Product.SellPrice);
        _eventBus.Publish(productDeletedIntegrationEvent);
        _logger.LogInformation("Published delete product event with ProductId:{productId}", domainEvent.Product.Id);
        return Task.CompletedTask;
    }
}