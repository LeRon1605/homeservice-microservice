using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.ProductAggregate.Events;

namespace Products.Application.DomainEventHandlers;

public class ProductAddedDomainEventHandler : IDomainEventHandler<ProductAddedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductAddedDomainEvent> _logger;
    
    public ProductAddedDomainEventHandler(IEventBus eventBus, ILogger<ProductAddedDomainEvent> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task Handle(ProductAddedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var productAddedIntegrationEvent = new ProductAddedIntegrationEvent(domainEvent.Product.Id, 
                                                                            domainEvent.Product.Name, 
                                                                            domainEvent.Product.ProductGroupId, 
                                                                            domainEvent.Product.SellPrice);
        _eventBus.Publish(productAddedIntegrationEvent);
        _logger.LogInformation("Published add product event with ProductId:{productId}", domainEvent.Product.Id);
        return Task.CompletedTask;
    }
}