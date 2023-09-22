using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record ProductUpdatedIntegrationEvent: IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public decimal? SellPrice { get; private set; }
    
    public ProductUpdatedIntegrationEvent(Guid id ,string name, Guid productGroupId, decimal? sellPrice)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        SellPrice = sellPrice;
    }
}