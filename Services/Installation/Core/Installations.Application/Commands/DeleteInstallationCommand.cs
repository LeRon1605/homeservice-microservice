using BuildingBlocks.Application.CQRS;

namespace Installations.Application.Commands;

public class DeleteInstallationCommand : ICommand
{
    public Guid InstallationId { get; init; } 
}