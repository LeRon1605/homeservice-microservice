using Newtonsoft.Json;

namespace Contracts.Application.Dtos.Contracts;

public class InstallationItemCreateDto
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