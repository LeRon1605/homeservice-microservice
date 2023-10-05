using BuildingBlocks.Domain.Specification;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class ContractOfCustomerSpecification : Specification<Contract>
{
    public ContractOfCustomerSpecification(Guid customerId, string? contractNo)
    {
        AddInclude(x=>x.Items);
        AddInclude(x=>x.Customer);
        AddFilter(x => x.CustomerId == customerId);
        if (!string.IsNullOrEmpty(contractNo))
        {
            AddFilter(x => x.No.ToString().Contains(contractNo.ToString()));
        }
    }
}