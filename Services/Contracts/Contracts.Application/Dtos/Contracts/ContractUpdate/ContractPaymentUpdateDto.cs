﻿namespace Contracts.Application.Dtos.Contracts.ContractUpdate;

public class ContractPaymentUpdateDto
{
    public DateTime DatePaid { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Surcharge { get; set; }
    public string? Reference { get; set; }
    public string? Comments { get; set; }
    public Guid? PaymentMethodId { get; set; }
}