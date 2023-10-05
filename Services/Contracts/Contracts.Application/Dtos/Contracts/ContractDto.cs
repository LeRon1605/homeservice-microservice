using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Application.Dtos.Contracts;

public class ContractDto
{
    public Guid Id { get; set; }
    public int No { get; set; }
    public int InstallationNo { get; set; } = 0;
    public string CustomerName { get; set; } = string.Empty;
    
    public decimal Value { get; set; }
    public decimal Balance { get; set; }
    
    public DateTime QuotedAt { get; set; }
    public DateTime? SoldAt { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(ContractStatus))]
    public ContractStatus Status { get; set; }
    
    public string SalePersonName { get; set; } = string.Empty; 
}