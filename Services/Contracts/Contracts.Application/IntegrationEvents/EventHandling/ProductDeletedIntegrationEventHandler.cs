using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.Products.DeleteProduct;
using Contracts.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.IntegrationEvents.EventHandling;

public class ProductDeletedIntegrationEventHandler : IIntegrationEventHandler<ProductDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductDeletedIntegrationEventHandler> _logger;

    public ProductDeletedIntegrationEventHandler(IMediator mediator, ILogger<ProductDeletedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Handle(ProductDeletedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event delete product: {IntegrationEventId} - ({@IntegrationEvent})",@event.ProductId, @event);
        
        var command = new DeleteProductCommand(@event.ProductId);
        _logger.LogInformation("Sending command: {commandName}", command);
        
        await _mediator.Send(command);
    }
}