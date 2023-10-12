using BuildingBlocks.Domain.Exceptions.Resource;

namespace Installations.Domain.InstallationAggregate.Exceptions;

public class InstallationNotFoundException : ResourceNotFoundException
{
    public InstallationNotFoundException(Guid installationId)
        : base(nameof(Installation), installationId, ErrorCodes.InstallationNotFound)
    {
    }
}