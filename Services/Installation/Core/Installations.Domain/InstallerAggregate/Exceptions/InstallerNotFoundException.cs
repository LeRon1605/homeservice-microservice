using BuildingBlocks.Domain.Exceptions.Resource;

namespace Installations.Domain.InstallerAggregate.Exceptions;

public class InstallerNotFoundException : ResourceNotFoundException
{
    public InstallerNotFoundException(Guid id) : base(nameof(Installer), id, ErrorCodes.InstallerNotFound)
    {
    } 
}