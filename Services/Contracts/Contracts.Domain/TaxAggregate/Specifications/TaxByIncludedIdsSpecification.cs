using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.TaxAggregate.Specifications;

public class TaxByIncludedIdsSpecification : Specification<Tax>
{
    public TaxByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(x => ids.Contains(x.Id));
    }
}