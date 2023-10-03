using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events;

public record ProductDeletedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; set; }

    public ProductDeletedIntegrationEvent(Guid productId)
    {
        ProductId = productId;
    }
}