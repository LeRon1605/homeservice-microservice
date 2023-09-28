using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Shopping.Application.Commands;
using Shopping.Application.IntegrationEvents.Events;

namespace Shopping.Application.IntegrationEvents.EventHandling;

public class ProductUnitUpdatedIntegrationEventHandler : IIntegrationEventHandler<ProductUnitUpdatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    
    public ProductUnitUpdatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(ProductUnitUpdatedIntegrationEvent @event)
    {
        var command = new UpdateProductUnitCommand(@event.Id, @event.Name);
        
        await _mediator.Send(command);
    }
}