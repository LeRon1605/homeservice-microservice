using BuildingBlocks.Domain.Exceptions.Common;

namespace Installations.Domain.InstallerAggregate.Exceptions;

public class InstallerIsDeactivatedException : InvalidInputException
{
    public InstallerIsDeactivatedException(Guid id) : base($"Installer with id: '{id}' is deactivated.")
    {
    }
}