using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Shopping.Application.IntegrationEvents.Events.Orders;
using Shopping.Domain.OrderAggregate.Events;

namespace Shopping.Application.DomainEventHandlers.Orders;

public class OrderAddedDomainEventHandler : IDomainEventHandler<OrderAddedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<OrderAddedDomainEventHandler> _logger;
    
    public OrderAddedDomainEventHandler(
        IEventBus eventBus,
        ILogger<OrderAddedDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }
    
    public Task Handle(OrderAddedDomainEvent @event, CancellationToken cancellationToken)
    {
        var orderAddedIntegrationEvent = new OrderAddedIntegrationEvent(
            @event.Order.Id,
            @event.Order.BuyerId,
            @event.Order.ContactInfo.CustomerName,
            @event.Order.ContactInfo.ContactName,
            @event.Order.ContactInfo.Email,
            @event.Order.ContactInfo.Phone,
            @event.Order.ContactInfo.Address,
            @event.Order.ContactInfo.City,
            @event.Order.ContactInfo.State,
            @event.Order.ContactInfo.PostalCode);

        _eventBus.Publish(orderAddedIntegrationEvent);
        
        _logger.LogInformation("Published integration event: {EventName}", orderAddedIntegrationEvent.GetType().Name);
        return Task.CompletedTask;
    }
}