using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events.Materials;

public record MaterialUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid MaterialId { get; set; }
    public string Name { get; set; }
    public bool IsObsolete { get; set; }

    public MaterialUpdatedIntegrationEvent(Guid materialId, string name, bool isObsolete)
    {
        Name = name;
        MaterialId = materialId;
        IsObsolete = isObsolete;
    } 
}