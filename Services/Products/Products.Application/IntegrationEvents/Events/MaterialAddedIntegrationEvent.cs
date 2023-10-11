using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record MaterialAddedIntegrationEvent : IntegrationEvent
{
    public Guid MaterialId { get; set; }
    public string Name { get; set; }
    public bool IsObsolete { get; set; }

    public MaterialAddedIntegrationEvent(Guid materialId, string name, bool isObsolete)
    {
        Name = name;
        MaterialId = materialId;
        IsObsolete = isObsolete;
    }
}