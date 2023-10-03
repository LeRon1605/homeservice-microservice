using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.Products.UpdateProduct;
using Contracts.Application.IntegrationEvents.Events.Products;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling.Products;

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
        _logger.LogInformation("Handling integration event update product: {IntegrationEventId} - ({@IntegrationEvent})",@event.ProductId, @event);
        
        var command = new UpdateProductCommand(@event.ProductId, @event.Name, @event.ProductGroupId, @event.ProductUnitId, @event.SellPrice, @event.Colors);
        
        _logger.LogInformation("Sending command: {commandName}", command);
        await _mediator.Send(command);
    }
}