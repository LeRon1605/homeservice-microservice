using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.PaymentMethodAggregate.Specifications;

public class PaymentMethodsByIncludedIdsSpecification : Specification<PaymentMethod>
{
    public PaymentMethodsByIncludedIdsSpecification(IEnumerable<Guid> ids)
    {
        AddFilter(x => ids.Contains(x.Id));
    }
}