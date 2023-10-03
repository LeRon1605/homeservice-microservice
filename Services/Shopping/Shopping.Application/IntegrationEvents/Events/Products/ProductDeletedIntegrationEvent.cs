using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events.Products;

public record ProductDeletedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; set; }

    public ProductDeletedIntegrationEvent(Guid productId)
    {
        ProductId = productId;
    }
}