using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Microsoft.Extensions.Logging;
using Shopping.Application.Commands.Products.UpdateProduct;
using Shopping.Application.IntegrationEvents.Events.Products;

namespace Shopping.Application.IntegrationEvents.EventHandling.Products;

public class ProductUpdatedIntegrationEventHandler : IIntegrationEventHandler<ProductUpdatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductUpdatedIntegrationEventHandler> _logger;

    public ProductUpdatedIntegrationEventHandler(IMediator mediator,
        ILogger<ProductUpdatedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ProductUpdatedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event update product: " +
                               "{IntegrationEventId} - ({@IntegrationEvent})",
            @event.ProductId, @event);
        
        var command = new UpdatedProductCommand(@event.ProductId, @event.Name, @event.ProductGroupId, @event.ProductUnitId, @event.SellPrice);
        
        _logger.LogInformation("Sending command: {commandName}", command);
        await _mediator.Send(command);
    }
}