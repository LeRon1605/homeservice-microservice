using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.ProductAggregate.Events;

namespace Products.Application.DomainEventHandlers;

public class ProductUpdatedDomainEventHandler : IDomainEventHandler<ProductUpdatedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductUpdatedDomainEventHandler> _logger;

    public ProductUpdatedDomainEventHandler(IEventBus eventBus, ILogger<ProductUpdatedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }
    public Task Handle(ProductUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var productUpdatedIntegrationEvent = new ProductUpdatedIntegrationEvent(
                                                domainEvent.Product.Id,
                                                domainEvent.Product.Name,
                                                domainEvent.Product.ProductGroupId,
                                                domainEvent.Product.SellPrice);
        _eventBus.Publish(productUpdatedIntegrationEvent);
        _logger.LogInformation("Published update product event with ProductId:{productId}", domainEvent.Product.Id);
        return Task.CompletedTask;
    }
}