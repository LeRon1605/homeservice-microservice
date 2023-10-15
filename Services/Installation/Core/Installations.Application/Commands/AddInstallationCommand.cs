using BuildingBlocks.Application.CQRS;
using Installations.Application.Dtos;

namespace Installations.Application.Commands;

public class AddInstallationCommand : ICommand
{
    public Guid ContractId { get; init; }
    public int ContractNo { get; init; }
    public Guid ContractLineId { get; init; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = null!;
    public string? ProductColor { get; init; }
    
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = null!;
    public Guid InstallerId { get; set; }
    
    public DateTime? InstallDate { get; init; }
    public DateTime? EstimatedStartTime { get; set; }
    public DateTime? EstimatedFinishTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualFinishTime { get; set; }
    
    public string? InstallationComment { get; set; }
    public string? FloorType { get; set; }
    public double InstallationMetres { get; set; }
    
    public InstallationAddressDto? InstallationAddress { get; init; }
    
    public List<InstallationItemCreateDto> InstallationItems { get; init; } = new();    
}