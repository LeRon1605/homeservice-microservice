using System.Text.Json.Serialization;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Application.Dtos.Contracts;

public class ContractDetailDto
{
    public Guid Id { get; set; }
    public int No { get; set; }
    
    public decimal ContractValue { get; set; }
    public decimal Balance { get; set; }
    public string? CustomerNote { get; set; }
    
    public EmployeeInContractDto SalePerson { get; set; } = null!;
    public EmployeeInContractDto? Supervisor { get; set; }
    public EmployeeInContractDto? CustomerServiceRep { get; set; }
    
    public int? PurchaseOrderNo { get; set; }
    
    public int? InvoiceNo { get; set; }
    public DateTime? InvoiceDate { get; set; }
    
    public DateTime QuotedAt { get; set; }
    public DateTime? SoldAt { get; set; }
    
    public Guid CustomerId { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ContractStatus Status { get; set; }
    
    public InstallationAddressDto InstallationAddress { get; set; } = null!;
    public DateTime? EstimatedInstallationDate { get; set; }
    public DateTime? ActualInstallationDate { get; set; }
}

public class EmployeeInContractDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}