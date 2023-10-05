using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class ContractDetailSpecification : Specification<Contract>
{
    public ContractDetailSpecification(Guid id)
    {
        AddFilter(x=>x.Id == id);
        AddInclude(x=>x.Items);
    }
}