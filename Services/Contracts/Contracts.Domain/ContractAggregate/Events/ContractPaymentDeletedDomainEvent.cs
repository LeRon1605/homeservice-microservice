using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.ContractAggregate.Events;

public class ContractPaymentDeletedDomainEvent : IDomainEvent
{
    public Contract Contract { get; set; }
    public ContractPayment ContractPayment { get; set; }
    
    public ContractPaymentDeletedDomainEvent(Contract contract, ContractPayment contractPayment)
    {
        Contract = contract;
        ContractPayment = contractPayment;
    }
}