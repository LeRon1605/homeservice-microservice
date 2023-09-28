using BuildingBlocks.Application.IntegrationEvent;

namespace Shopping.Application.IntegrationEvents.Events;

public record ProductAddedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public string? ProductUnitName { get; private set; }
    public decimal SellPrice { get; private set; }

    public ProductAddedIntegrationEvent(
        Guid id, 
        string name, 
        Guid productGroupId, 
        Guid? productUnitId, 
        decimal sellPrice)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
        ProductUnitName = ProductUnitName;
    }
}