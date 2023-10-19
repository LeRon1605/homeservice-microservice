using BuildingBlocks.Domain.Specification;
using Contracts.Domain.EmployeeAggregate;

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
        AddInclude(x => x.ActionByEmployee!);
        AddFilter(x => x.ContractId == contractId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchField(nameof(ContractAction.Name));
            AddSearchField(nameof(ContractAction.CreatedBy));
            AddSearchField(nameof(ContractAction.LastModifiedBy));
            AddSearchField($"{nameof(ContractAction.ActionByEmployee)}.{nameof(Employee.Name)}");
            
            AddSearchTerm(search);
        }
    }
}