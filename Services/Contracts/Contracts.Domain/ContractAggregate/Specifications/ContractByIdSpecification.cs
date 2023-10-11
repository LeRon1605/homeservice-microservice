using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class ContractByIdSpecification : Specification<Contract>
{
    public ContractByIdSpecification(Guid id)
    {
        AddFilter(x => x.Id == id);
        AddInclude(x => x.Items);
        AddInclude(x => x.Payments);
    }
}