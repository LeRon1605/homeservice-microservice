namespace Contracts.Application.Dtos.Contracts;

public class ContractPaymentDto
{
    public Guid Id { get; set; }
    public DateTime DatePaid { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Surcharge { get; set; }
    public decimal TotalPaid { get; set; }
    
    public Guid ContractId { get; set; }

    public PaymentMethodInContractPaymentDto PaymentMethod { get; set; } = null!;
    
    public bool IsDeleted { get; set; }
    
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? DeletedBy { get; set; }
}

public class PaymentMethodInContractPaymentDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
}