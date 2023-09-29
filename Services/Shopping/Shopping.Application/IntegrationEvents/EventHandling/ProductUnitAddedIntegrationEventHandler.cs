using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Shopping.Application.Commands;
using Shopping.Application.Commands.ProductUnits.AddProductUnits;
using Shopping.Application.IntegrationEvents.Events;

namespace Shopping.Application.IntegrationEvents.EventHandling;

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