using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.ContractAggregate.Specifications;

public class GetContractsWithContractLineSpecification : Specification<Contract>
{
    public GetContractsWithContractLineSpecification()
    {
        AddInclude(x => x.ContractLines);
    } 
}
