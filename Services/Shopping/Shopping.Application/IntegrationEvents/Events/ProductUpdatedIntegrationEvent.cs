using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public string? ProductUnitName { get; private set; }
    public decimal SellPrice { get; private set; }

    public ProductUpdatedIntegrationEvent(
        Guid id,
        string name, 
        Guid productGroupId,
        decimal sellPrice,
        Guid? productUnitId, 
        string? productUnitName)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
        ProductUnitName = productUnitName;
    }
}