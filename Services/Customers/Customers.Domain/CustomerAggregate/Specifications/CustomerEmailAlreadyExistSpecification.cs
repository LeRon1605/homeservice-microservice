using BuildingBlocks.Domain.Specification;

namespace Customers.Domain.CustomerAggregate.Specifications;

public class CustomerEmailAlreadyExistSpecification : Specification<Customer>
{
    public CustomerEmailAlreadyExistSpecification(string? email, Guid? customerId = null)
    {
        if (!string.IsNullOrWhiteSpace(email))
            AddFilter(customer => customer.Email == email);
        
        if (customerId.HasValue)
            AddFilter(customer => customer.Id != customerId.Value);
    } 
}