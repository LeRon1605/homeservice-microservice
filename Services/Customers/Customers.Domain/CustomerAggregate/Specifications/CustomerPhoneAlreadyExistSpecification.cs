using BuildingBlocks.Domain.Specification;

namespace Customers.Domain.CustomerAggregate.Specifications;

public class CustomerPhoneAlreadyExistSpecification : Specification<Customer>
{
    public CustomerPhoneAlreadyExistSpecification(string? phone, Guid? customerId = null)
    {
        if (!string.IsNullOrWhiteSpace(phone))
            AddFilter(customer => customer.Phone == phone);
        
        if (customerId.HasValue)
            AddFilter(customer => customer.Id != customerId.Value);
    } 
}