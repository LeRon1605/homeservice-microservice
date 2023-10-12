namespace Installations.Application.Dtos;

public class InstallationDetailDto
{
    public Guid Id { get; set; } 
    public int No { get; set; }
    public Guid ContractId { get; set; }
    public int ContractNo { get; set; }
    public Guid ContractLineId { get; set; } 
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? ProductColor { get; set;}
    
    public Guid InstallerId { get; set; }
    
    public string? InstallationComment { get; set; }
    public string? FloorType { get; set; }
    public double InstallationMetres { get; set; }
    
    public DateTime? InstallDate { get; init; }
    public DateTime? EstimatedStartTime { get; private set; }
    public DateTime? EstimatedFinishTime { get; private set; }
    public DateTime? ActualStartTime { get; private set; }
    public DateTime? ActualFinishTime { get; private set; }
    
    public List<InstallationItemDto> Items { get; set; } = new();
}