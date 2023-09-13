using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Customers.Domain.CustomerAggregate.Specifications;

public class CustomerFilterSpecification : Specification<Customer>, ISpecification<Customer>
{
    private readonly string? _search;
    
    public CustomerFilterSpecification(string? search, int pageIndex, int pageSize)
    {
        _search = search;
        ApplyPaging(pageIndex, pageSize);
    }

    public override Expression<Func<Customer, bool>> ToExpression()
    {
        return string.IsNullOrWhiteSpace(_search)
            ? p => true
            : p => p.CustomerName.ToLower().Contains(_search.ToLower());
    }
}