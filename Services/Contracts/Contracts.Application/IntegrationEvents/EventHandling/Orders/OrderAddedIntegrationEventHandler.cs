using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.PendingOrders.AddPendingOrder;
using Contracts.Application.IntegrationEvents.Events.Orders;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling.Orders;

public class OrderAddedIntegrationEventHandler : IIntegrationEventHandler<OrderAddedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderAddedIntegrationEventHandler> _logger;
    public OrderAddedIntegrationEventHandler(IMediator mediator, ILogger<OrderAddedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(OrderAddedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event add order: {IntegrationEventId} -  ({@IntegrationEvent})", @event.Id, @event);

        var command = new AddPendingOrderCommand(
            @event.OrderId, 
            @event.BuyerId, 
            @event.CustomerName,
            @event.ContactName, 
            @event.Email, 
            @event.Phone, 
            @event.Address, 
            @event.City, 
            @event.State,
            @event.PostalCode);
        
        _logger.LogInformation("Send command {Command}", command.GetType().Name);
        await _mediator.Send(command);
    }
}