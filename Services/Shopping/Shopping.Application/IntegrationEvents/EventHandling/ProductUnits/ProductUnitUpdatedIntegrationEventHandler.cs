using BuildingBlocks.Application.IntegrationEvent;
using MediatR;
using Shopping.Application.Commands.ProductUnits.UpdateProductUnit;
using Shopping.Application.IntegrationEvents.Events.ProductUnits;

namespace Shopping.Application.IntegrationEvents.EventHandling.ProductUnits;

public class ProductUnitUpdatedIntegrationEventHandler : IIntegrationEventHandler<ProductUnitUpdatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    
    public ProductUnitUpdatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(ProductUnitUpdatedIntegrationEvent @event)
    {
        var command = new UpdateProductUnitCommand(@event.ProductUnitId, @event.Name);
        
        await _mediator.Send(command);
    }
}