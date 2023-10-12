using Newtonsoft.Json;

namespace Contracts.Application.Dtos.Contracts;

public class InstallationCreateDto
{
    public Guid ProductId { get; set; }
    [JsonIgnore]
    public string? ProductName { get; set; } = null!;
    public string? Color { get; set; }
    
    public DateTime? InstallDate { get; init; }
    public DateTime? EstimatedStartTime { get; set; }
    public DateTime? EstimatedFinishTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualFinishTime { get; set; }
    
    public string? InstallationComment { get; set; }
    public string? FloorType { get; set; }
    
    public Guid InstallerId { get; set; }
    
    public double InstallationMetres { get; set; }

    public List<InstallationItemCreateDto> Items { get; set; } = new();
}