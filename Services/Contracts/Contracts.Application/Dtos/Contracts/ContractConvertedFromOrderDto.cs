using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Application.Dtos.Contracts;

public class ContractConvertedFromOrderDto
{
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

    [MinLength(1)]
    public List<ContractLineCreateDto> Items { get; set; } = null!;
    public List<ContractPaymentCreateDto>? Payments { get; set; }
}