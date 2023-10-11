using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Contracts.Domain.PaymentMethodAggregate;

namespace Contracts.Domain.ContractAggregate;

public class ContractPayment : AuditableEntity
{
    public DateTime DatePaid { get; private set; }
    public decimal PaidAmount { get; private set; }
    public decimal Surcharge { get; private set; }
    public string? Reference { get; private set; }
    public string? Comments { get; private set; }
    
    public Guid ContractId { get; private set; }
    
    public Guid? PaymentMethodId { get; private set; }
    
    public string? PaymentMethodName { get; private set; }
    
    public ContractPayment(
        Guid contractId, 
        DateTime datePaid, 
        decimal paidAmount, 
        decimal? surcharge, 
        string? reference, 
        string? comments,
        Guid? paymentMethodId,
        string? paymentMethodName)
    {
        ContractId = Guard.Against.Null(contractId, nameof(ContractId));
        DatePaid = Guard.Against.Null(datePaid, nameof(DatePaid));
        PaidAmount = Guard.Against.Negative(paidAmount, nameof(PaidAmount));
        Surcharge = surcharge.HasValue ? Guard.Against.Negative(surcharge.Value, nameof(Surcharge)) : 0;
        Reference = reference;
        Comments = comments;
        PaymentMethodId = paymentMethodId;
        PaymentMethodName = paymentMethodName;
    }

    public void Update(
        DateTime datePaid, 
        decimal paidAmount, 
        decimal? surcharge, 
        string? reference, 
        string? comments,
        Guid? paymentMethodId,
        string? paymentMethodName)
    {
        DatePaid = Guard.Against.Null(datePaid, nameof(DatePaid));
        PaidAmount = Guard.Against.Negative(paidAmount, nameof(PaidAmount));
        Surcharge = surcharge.HasValue ? Guard.Against.Negative(surcharge.Value, nameof(Surcharge)) : 0;
        Reference = reference;
        Comments = comments;
        PaymentMethodId = paymentMethodId;
        PaymentMethodName = paymentMethodName;
    }

    private ContractPayment()
    {
        
    }
}