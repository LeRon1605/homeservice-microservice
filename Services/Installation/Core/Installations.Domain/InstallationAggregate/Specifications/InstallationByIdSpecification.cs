using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.InstallationAggregate.Specifications;

public class InstallationByIdSpecification : Specification<Installation>
{
    public InstallationByIdSpecification(Guid installationId)
    {
        AddInclude(i => i.Items);

        AddFilter(i => i.Id == installationId);
    }
    
}