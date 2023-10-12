namespace Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;

public class InstallationEventDto
{
    public Guid ContractLineId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
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

    public List<InstallationItemEventDto> Items { get; set; } = new();
}