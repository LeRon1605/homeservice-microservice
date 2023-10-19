using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.ContractAggregate.Events;

public class ContractItemDeletedDomainEvent : IDomainEvent
{
    public Contract Contract { get; set; }
    public ContractLine ContractLine { get; set; }
    
    public ContractItemDeletedDomainEvent(Contract contract, ContractLine contractLine)
    {
        Contract = contract;
        ContractLine = contractLine;
    }
}