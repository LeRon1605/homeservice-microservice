using BuildingBlocks.Domain.Specification;

namespace Customers.Domain.CustomerAggregate.Specifications;

public class CustomerFilterSpecification : Specification<Customer>
{
    public CustomerFilterSpecification(string? search, int pageIndex, int pageSize)
    {
        ApplyPaging(pageIndex, pageSize);

        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Customer.Name));
        }
    }
}