using BuildingBlocks.Application.CQRS;
using Installations.Application.Dtos;

namespace Installations.Application.Commands;

public class UpdateInstallationAddressCommand : ICommand
{
    public Guid ContractId { get; init; } 
    
    public InstallationAddressDto InstallationAddress { get; init; } = null!;
}