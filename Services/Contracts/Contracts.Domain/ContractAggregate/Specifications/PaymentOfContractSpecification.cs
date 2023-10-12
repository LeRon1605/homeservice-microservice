using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class PaymentOfContractSpecification : Specification<ContractPayment>
{
    public PaymentOfContractSpecification(
        string? search, 
        int pageSize,
        int pageIndex,
        Guid contractId,
        bool showDeleted)
    {
        ApplyPaging(pageIndex, pageSize);
        AddFilter(x => x.ContractId == contractId);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchField(nameof(ContractPayment.PaymentMethodName));
            AddSearchField(nameof(ContractPayment.CreatedBy));
            AddSearchField(nameof(ContractPayment.LastModifiedBy));
            if (showDeleted)
            {
                AddSearchField(nameof(ContractPayment.DeletedBy));
            }
            
            AddSearchTerm(search);
        }
        
        if (showDeleted)
        {
            IgnoreSoftDelete();    
        }
    }
}