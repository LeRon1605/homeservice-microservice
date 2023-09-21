using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Customers.Domain.CustomerAggregate;

namespace Customers.Infrastructure.EfCore.Repositories;

public class CustomerRepository : EfCoreRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}