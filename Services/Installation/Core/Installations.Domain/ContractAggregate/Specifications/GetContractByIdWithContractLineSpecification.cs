using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.ContractAggregate.Specifications;

public class GetContractByIdWithContractLineSpecification : Specification<Contract>
{
    public GetContractByIdWithContractLineSpecification(Guid contractId)
    {
        AddInclude(x => x.ContractLines);
        AddFilter(x => x.Id == contractId);
    } 
}