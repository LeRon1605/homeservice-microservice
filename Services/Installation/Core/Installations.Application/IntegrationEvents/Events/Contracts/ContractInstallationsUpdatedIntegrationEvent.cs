using BuildingBlocks.Application.IntegrationEvent;
using Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;

namespace Installations.Application.IntegrationEvents.Events.Contracts;

public record ContractInstallationsUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid ContractId {get; init; } 
    public int ContractNo { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = null!;
    
    public InstallationAddressEventDto? InstallationAddress { get; init; }
    
    public List<InstallationUpdatedEventDto> Installations { get; set; } = new(); 
    public bool IsAddressChanged { get; init; }
}