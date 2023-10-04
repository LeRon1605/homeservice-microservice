using BuildingBlocks.Domain.Specification;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Domain.CustomerAggregate.Specifications;

public class CustomerHasContractSpecification : Specification<Contract>
{
    public CustomerHasContractSpecification(Guid customerId)
    {
        AddFilter(c => c.CustomerId == customerId);
    } 
}