using BuildingBlocks.Application.CQRS;
using Installations.Application.Dtos;
using Newtonsoft.Json;

namespace Installations.Application.Commands;

public class UpdateInstallationCommand : ICommand<InstallationDto>
{
    [JsonIgnore]
    public Guid InstallationId { get; set; }
    public Guid ContractLineId { get; init; }
    
    public Guid InstallerId { get; set; }
    
    public DateTime? InstallDate { get; init; }
    public DateTime? EstimatedStartTime { get; set; }
    public DateTime? EstimatedFinishTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualFinishTime { get; set; }
    
    public string? InstallationComment { get; set; }
    public string? FloorType { get; set; }
    public double InstallationMetres { get; set; }
    
    public List<InstallationItemUpdateDto> InstallationItems { get; init; } = new();    
}