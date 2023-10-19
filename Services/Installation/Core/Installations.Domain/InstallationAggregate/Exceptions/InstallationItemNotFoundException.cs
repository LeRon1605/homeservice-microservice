using BuildingBlocks.Domain.Exceptions.Resource;

namespace Installations.Domain.InstallationAggregate.Exceptions;

public class InstallationItemNotFoundException : ResourceNotFoundException
{
    public InstallationItemNotFoundException(Guid materialId) : base($"Product with material id ({materialId}) not found", ErrorCodes.InstallationItemNotFound)
    {
    } 
}