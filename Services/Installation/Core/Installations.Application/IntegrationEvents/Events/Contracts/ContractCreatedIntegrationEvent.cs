using BuildingBlocks.Application.IntegrationEvent;
using Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;

namespace Installations.Application.IntegrationEvents.Events.Contracts;

public record ContractCreatedIntegrationEvent : IntegrationEvent
{
    public Guid ContractId {get; init; } 
    public Guid CustomerId { get; init; }
    public Guid? SalespersonId { get; init; }
    public Guid? SupervisorId { get; init; }
    
    public InstallationAddressEventDto? InstallationAddress { get; init; }
    
    public List<InstallationEventDto> Installations { get; set; } = new(); 
}