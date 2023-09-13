using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Shopping.Domain.ShoppingAggregate;

namespace Shopping.Infrastructure.EfCore.Repositories;

public class ReadOnlyOrderRepository: EfCoreReadOnlyRepository<OrderDbContext,Order>, IReadOnlyOrderRepository
{
    public ReadOnlyOrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}