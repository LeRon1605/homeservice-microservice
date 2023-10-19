using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.ContractAggregate.Events;

public class NewContractItemAddedDomainEvent : IDomainEvent
{
    public Contract Contract { get; set; }
    public ContractLine ContractLine { get; set; }
    
    public NewContractItemAddedDomainEvent(Contract contract, ContractLine contractLine)
    {
        Contract = contract;
        ContractLine = contractLine;
    }
}