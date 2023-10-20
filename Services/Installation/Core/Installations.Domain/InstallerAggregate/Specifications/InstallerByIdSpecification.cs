using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.InstallerAggregate.Specifications;

public class InstallerByIdSpecification : Specification<Installer>
{
    public InstallerByIdSpecification(Guid id)
    {
        AddFilter(installer => installer.Id == id);
    }
}