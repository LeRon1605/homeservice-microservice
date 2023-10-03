using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.ProductUnits.AddProductUnits;
using Contracts.Application.IntegrationEvents.Events.ProductUnits;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling.ProductUnits;

public class ProductUnitAddedIntegrationEventHandler : IIntegrationEventHandler<ProductUnitAddedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductUnitAddedIntegrationEventHandler> _logger;

    public ProductUnitAddedIntegrationEventHandler(IMediator mediator, ILogger<ProductUnitAddedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ProductUnitAddedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event add product: {IntegrationEventId} -  ({@IntegrationEvent})", @event.Id, @event);

        var command = new AddProductUnitCommand(@event.ProductUnitId, @event.Name);
        
        _logger.LogInformation("Sending command: {commandName}", command);
        
        await _mediator.Send(command);
    }
}