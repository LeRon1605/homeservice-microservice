using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class ContractLineByContractSpecification : Specification<ContractLine>
{
    public ContractLineByContractSpecification(Guid contractId)
    {
        AddFilter(x => x.ContractId == contractId);
    }
}