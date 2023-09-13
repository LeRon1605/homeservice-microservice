using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Customers.Domain.CustomerAggregate;

namespace Customers.Infrastructure.Repositories;

public class CustomerRepository : EfCoreRepository<CustomerDbContext,Customer> ,ICustomerRepository
{
    public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
    {
    }
}