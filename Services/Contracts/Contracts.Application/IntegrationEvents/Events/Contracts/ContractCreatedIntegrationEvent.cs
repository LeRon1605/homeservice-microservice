using BuildingBlocks.Application.IntegrationEvent;
using Contracts.Application.IntegrationEvents.Events.Contracts.EventDtos;

namespace Contracts.Application.IntegrationEvents.Events.Contracts;

public record ContractCreatedIntegrationEvent : IntegrationEvent
{
    public Guid ContractId {get; init; } 
    public int ContractNo { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = null!;
    
    public InstallationAddressEventDto? InstallationAddress { get; init; }
    public ICollection<ContractLineEventDto> ContractLines { get; init; } = null!;
}