using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Shopping.Application.Commands;
using Shopping.Application.Commands.AddProduct;
using Shopping.Application.IntegrationEvents.Events;

namespace Shopping.Application.IntegrationEvents.EventHandling;

public class ProductAddedIntegrationEventHandler : IIntegrationEventHandler<ProductAddedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductAddedIntegrationEventHandler> _logger;

    public ProductAddedIntegrationEventHandler(IMediator mediator, ILogger<ProductAddedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ProductAddedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event add product: {IntegrationEventId} -  ({@IntegrationEvent})", @event.ProductId, @event);

        var command = new AddedProductCommand(
            @event.ProductId, 
            @event.Name, 
            @event.ProductGroupId,
            @event.SellPrice,
            @event.ProductUnitId);
        
        _logger.LogInformation("Sending command: {commandName}", command);

        await _mediator.Send(command);
    }
}