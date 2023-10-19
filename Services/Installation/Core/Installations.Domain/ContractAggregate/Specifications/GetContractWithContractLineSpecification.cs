using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.ContractAggregate.Specifications;

public class GetContractWithContractLineSpecification : Specification<Contract>
{
    public GetContractWithContractLineSpecification(Guid contractId)
    {
        AddInclude(x => x.ContractLines);
        AddFilter(x => x.Id == contractId);
    } 
}