using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Shopping.Application.Commands.Orders.FinishOrder;
using Shopping.Application.IntegrationEvents.Events.Orders;

namespace Shopping.Application.IntegrationEvents.EventHandling.Orders;

public class OrderFinishedIntegrationEventHandler : IIntegrationEventHandler<OrderFinishedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderFinishedIntegrationEventHandler> _logger;

    public OrderFinishedIntegrationEventHandler(IMediator mediator, ILogger<OrderFinishedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public Task Handle(OrderFinishedIntegrationEvent @event)
    {
        var command = new FinishOrderCommand(@event.OrderId);
        _logger.LogInformation("Send {Command} to finish order {OrderId}", command.GetType().Name, @event.OrderId);
        
        return _mediator.Send(command);
    }
}