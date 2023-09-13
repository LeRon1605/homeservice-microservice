using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Shopping.Domain.ShoppingAggregate;

namespace Shopping.Infrastructure.EfCore.Repositories;

public class OrderRepository: EfCoreRepository<OrderDbContext,Order> ,IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}