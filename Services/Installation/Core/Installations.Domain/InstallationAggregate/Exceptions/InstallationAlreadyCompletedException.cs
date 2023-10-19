using BuildingBlocks.Domain.Exceptions.Common;

namespace Installations.Domain.InstallationAggregate.Exceptions;

public class InstallationAlreadyCompletedException : InvalidInputException
{
    public InstallationAlreadyCompletedException(Guid installationId)
        : base($"Installation with id: '{installationId}' is already completed.", ErrorCodes.InstallationAlreadyCompleted)
    {
    } 
}