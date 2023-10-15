using System.Text.Json.Serialization;

namespace Contracts.Application.Dtos.Contracts.ContractUpdate;

public class InstallationItemUpdateDto
{
    public Guid MaterialId { get; set; }
    [JsonIgnore]
    public string? MaterialName { get; set; }
    public int Quantity { get; set; }
    
    public decimal Cost { get; set; }
    public decimal SellPrice { get; set; }
    public Guid UnitId { get; set; }
    [JsonIgnore]
    public string? UnitName { get; set; }
}