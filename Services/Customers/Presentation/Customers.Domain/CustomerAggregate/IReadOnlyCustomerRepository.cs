using BuildingBlocks.Domain.Data;

namespace Customers.Domain.CustomerAggregate;

public interface IReadOnlyCustomerRepository : IReadOnlyRepository<Customer>
{
    
}