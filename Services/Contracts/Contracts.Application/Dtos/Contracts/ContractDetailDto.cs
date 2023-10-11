﻿using System.Text.Json.Serialization;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Application.Dtos.Contracts;

public class ContractDetailDto
{
    public Guid Id { get; set; }
    public int No { get; set; }
    
    public decimal ContractValue { get; set; }
    public decimal Balance { get; set; }
    public string? CustomerNote { get; set; }
    
    public Guid SalePersonId { get; set; }
    public Guid? SupervisorId { get; set; }
    public Guid? CustomerServiceRepId { get; set; }
    
    public int? PurchaseOrderNo { get; set; }
    
    public int? InvoiceNo { get; set; }
    public DateTime? InvoiceDate { get; set; }
    
    public DateTime QuotedAt { get; set; }
    public DateTime? SoldAt { get; set; }
    
    public Guid CustomerId { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ContractStatus Status { get; set; }

    public IEnumerable<ContractLineDto> Items { get; set; } = null!;
    public IEnumerable<ContractPaymentDto> Payments { get; set; } = null!;
    public IEnumerable<ContractActionDto> Actions { get; set; } = null!;
    
    public InstallationAddressDto InstallationAddress { get; set; } = null!;
    public DateTime? EstimatedInstallationDate { get; set; }
    public DateTime? ActualInstallationDate { get; set; }
}