using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.Commands.ProductUnits.UpdateProductUnit;
using Contracts.Application.IntegrationEvents.Events.ProductUnits;
using MediatR;

namespace Contracts.Application.IntegrationEvents.EventHandling.ProductUnits;

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