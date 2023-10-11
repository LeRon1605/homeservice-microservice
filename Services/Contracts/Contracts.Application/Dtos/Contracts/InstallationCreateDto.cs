namespace Contracts.Application.Dtos.Contracts;

public class InstallationCreateDto
{
    public Guid ProductId { get; set; }
    public string? Color { get; set; }
    
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