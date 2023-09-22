using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record ProductAddedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductTypeId { get; private set; }
    public decimal? SellPrice { get; private set; }
    
    public ProductAddedIntegrationEvent(Guid id ,string name, Guid productTypeId, decimal? sellPrice)
    {
        Id = id;
        Name = name;
        ProductTypeId = productTypeId;
        SellPrice = sellPrice;
    }
}