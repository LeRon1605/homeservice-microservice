using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record MaterialUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid MaterialId { get; set; }
    public string Name { get; set; }
    
    public Guid ProductTypeId { get; private set; }
    
    public Guid? SellUnitId { get; private set; }
    public string? SellUnitName { get; private set; }
    
    public decimal SellPrice { get; private set; }
    public decimal? Cost { get; private set; }
    
    public bool IsObsolete { get; private set; }
    
    public MaterialUpdatedIntegrationEvent(Guid materialId, 
                                           string name, 
                                           Guid productTypeId, 
                                           Guid? sellUnitId, 
                                           string? sellUnitName,
                                           decimal sellPrice, 
                                           decimal? cost, 
                                           bool isObsolete)
    {
        MaterialId = materialId;
        Name = name;
        ProductTypeId = productTypeId;
        SellUnitId = sellUnitId;
        SellUnitName = sellUnitName;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
}