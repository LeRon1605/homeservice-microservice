using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events.Materials;

public record MaterialDeletedIntegrationEvent : IntegrationEvent
{
    public Guid MaterialId { get; set; }

    public MaterialDeletedIntegrationEvent(Guid materialId)
    {
        MaterialId = materialId;
    } 
}