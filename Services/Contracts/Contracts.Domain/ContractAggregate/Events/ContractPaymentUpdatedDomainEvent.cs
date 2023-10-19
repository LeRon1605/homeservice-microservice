using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.ContractAggregate.Events;

public class ContractPaymentUpdatedDomainEvent : IDomainEvent
{
    public Contract Contract { get; set; }
    public ContractPayment ContractPayment { get; set; }
    
    public ContractPaymentUpdatedDomainEvent(Contract contract, ContractPayment contractPayment)
    {
        Contract = contract;
        ContractPayment = contractPayment;
    }
}