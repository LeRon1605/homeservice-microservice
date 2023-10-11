using BuildingBlocks.Application.CQRS;
using Installations.Application.Dtos;

namespace Installations.Application.Commands;

public class AddInstallationCommand : ICommand
{
    public Guid ContractId { get; init; }
    public Guid ContractLineId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid? SalespersonId { get; init; }
    public Guid? SupervisorId { get; init; }
    public Guid InstallerId { get; set; }
    
    public string? FullAddress { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? PostalCode { get; init; }
    
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