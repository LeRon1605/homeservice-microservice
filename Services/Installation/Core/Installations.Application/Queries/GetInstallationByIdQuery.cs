using BuildingBlocks.Application.CQRS;
using Installations.Application.Dtos;

namespace Installations.Application.Queries;

public class GetInstallationByIdQuery : IQuery<InstallationDetailDto>
{
    public Guid InstallationId { get; set; } 
    
    public GetInstallationByIdQuery(Guid installationId)
    {
        InstallationId = installationId;
    }
}