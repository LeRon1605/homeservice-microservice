﻿using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Customers.Domain.CustomerAggregate;

namespace Customers.Infrastructure.EfCore.Repositories;

public class ReadOnlyCustomerRepository : EfCoreReadOnlyRepository<CustomerDbContext,Customer>, IReadOnlyCustomerRepository
{
    public ReadOnlyCustomerRepository(CustomerDbContext dbContext) : base(dbContext)
    {
    }
}