using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Application.Dtos.Contracts;

public class ContractsOfCustomerDto
{
    public int ContractNo { get; set; }
    public int InstallationNo { get; set; }
    public decimal ContractValue { get; set; }
    public decimal Balance { get; set; }
    public string SalePersonName { get; set; } = string.Empty;
    public DateTime QuotedAt { get; set; }
    public DateTime? SoldAt { get; set; }
    public ContractStatus Status { get; set; }
}