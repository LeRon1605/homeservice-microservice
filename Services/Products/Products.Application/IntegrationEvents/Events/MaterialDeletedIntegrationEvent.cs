using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record MaterialDeletedIntegrationEvent : IntegrationEvent
{
    public Guid MaterialId { get; set; }

    public MaterialDeletedIntegrationEvent(Guid materialId)
    {
        MaterialId = materialId;
    } 
}