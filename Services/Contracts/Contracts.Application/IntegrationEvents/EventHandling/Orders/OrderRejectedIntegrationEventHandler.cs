using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands;
using Contracts.Application.Commands.PendingOrders.DeletePendingOrder;
using Contracts.Application.IntegrationEvents.Events.Orders;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling.Orders;

public class OrderRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderRejectedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderRejectedIntegrationEventHandler> _logger;

    public OrderRejectedIntegrationEventHandler(ILogger<OrderRejectedIntegrationEventHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    public async Task Handle(OrderRejectedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event reject order: " +
                               "{IntegrationEventId} - ({@IntegrationEvent})", 
            @event.Id, @event);
        
        var command = new DeletePendingOrderCommand(@event.Id);
        _logger.LogInformation("Sending command: {commandName}", command);
        
        await _mediator.Send(command);
    }
}