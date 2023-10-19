using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.ContractAggregate.Events;

public class ContractItemUpdatedDomainEvent : IDomainEvent
{
    public Contract Contract { get; set; }
    public ContractLine ContractLine { get; set; }
    
    public ContractItemUpdatedDomainEvent(Contract contract, ContractLine contractLine)
    {
        Contract = contract;
        ContractLine = contractLine;
    }
}