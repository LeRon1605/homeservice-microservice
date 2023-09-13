using BuildingBlocks.Domain.Data;

namespace Shopping.Domain.ShoppingAggregate;

public interface IReadOnlyOrderRepository: IReadOnlyRepository<Order>
{
    
}