using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Shopping.Application.IntegrationEvents.Events;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.DomainEventHandlers;

public class OrderRejectDomainEventHandler : IDomainEventHandler<OrderRejectDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<OrderRejectDomainEventHandler> _logger;

    public OrderRejectDomainEventHandler(IEventBus eventBus, ILogger<OrderRejectDomainEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task Handle(OrderRejectDomainEvent notification, CancellationToken cancellationToken)
    {
        var orderRejectIntegrationEvent = new OrderRejectedIntegrationEvent(notification.Order.Id);
        _eventBus.Publish(orderRejectIntegrationEvent);
        _logger.LogInformation("Published reject order event with OrderId: {orderRejectId}",
            notification.Order.Id);
        return Task.CompletedTask;
    }
}