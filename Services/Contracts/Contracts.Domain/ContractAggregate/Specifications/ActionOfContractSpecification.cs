using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class ActionOfContractSpecification : Specification<ContractAction>
{
    public ActionOfContractSpecification(
        string? search, 
        int pageSize,
        int pageIndex,
        Guid contractId)
    {
        ApplyPaging(pageIndex, pageSize);
        AddFilter(x => x.ContractId == contractId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchField(nameof(ContractAction.Name));
            AddSearchField(nameof(ContractAction.CreatedBy));
            AddSearchField(nameof(ContractAction.LastModifiedBy));
            
            AddSearchTerm(search);
        }
    }
}