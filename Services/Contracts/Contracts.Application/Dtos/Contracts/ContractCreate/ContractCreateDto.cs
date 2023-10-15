using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Application.Dtos.Contracts.ContractCreate;

public class ContractCreateDto
{
    public Guid CustomerId { get; set; }
    public string? CustomerNote { get; set; }
    
    public Guid SalePersonId { get; set; }
    public Guid? SupervisorId { get; set; }
    public Guid? CustomerServiceRepId { get; set; }
    
    public int? PurchaseOrderNo { get; set; }
    
    public int? InvoiceNo { get; set; }
    public DateTime? InvoiceDate { get; set; }
    
    public DateTime? EstimatedInstallationDate { get; set; }
    public DateTime? ActualInstallationDate { get; set; }
    public InstallationAddressDto InstallationAddress { get; set; } = null!;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ContractStatus Status { get; set; }
    
    public DateTime? SoldAt { get; set; }

    [MinLength(1)]
    public List<ContractLineCreateDto> Items { get; set; } = null!;
    public List<ContractPaymentCreateDto>? Payments { get; set; }
    public List<ContractActionCreateDto>? Actions { get; set; }
    public List<InstallationCreateDto>? Installations { get; set; }
}