using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Shopping.Application.Commands.ProductUnits.DeleteProductUnit;
using Shopping.Application.IntegrationEvents.Events.ProductUnits;

namespace Shopping.Application.IntegrationEvents.EventHandling.ProductUnits;

public class ProductUnitDeletedIntegrationEventHandler : IIntegrationEventHandler<ProductUnitDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductUnitDeletedIntegrationEventHandler> _logger;

    public ProductUnitDeletedIntegrationEventHandler(IMediator mediator, ILogger<ProductUnitDeletedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(ProductUnitDeletedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event delete product: " +
                               "{IntegrationEventId} - ({@IntegrationEvent})", 
            @event.Id, @event);
        
        var command = new DeleteProductUnitCommand(@event.ProductUnitId);
        _logger.LogInformation("Sending command: {commandName}", command);
        
        await _mediator.Send(command);
    }
}