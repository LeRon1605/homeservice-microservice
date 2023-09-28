using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductAddedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public decimal SellPrice { get; private set; }

    public ProductAddedIntegrationEvent(Guid productId, string name, Guid productGroupId,
        Guid? productUnitId, decimal sellPrice)
    {
        ProductId = productId;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
    }
}