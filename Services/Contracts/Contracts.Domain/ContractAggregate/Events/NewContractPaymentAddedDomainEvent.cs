using BuildingBlocks.Domain.Event;

namespace Contracts.Domain.ContractAggregate.Events;

public class NewContractPaymentAddedDomainEvent : IDomainEvent
{
    public Contract Contract { get; set; }
    public ContractPayment ContractPayment { get; set; }
    
    public NewContractPaymentAddedDomainEvent(Contract contract, ContractPayment contractPayment)
    {
        Contract = contract;
        ContractPayment = contractPayment;
    }
}