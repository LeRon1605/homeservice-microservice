using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class FinishedOrderLineByProductIdSpecification : Specification<OrderLine>
{
    public FinishedOrderLineByProductIdSpecification(Guid productId)
    {
        AddInclude(x => x.Order);
        AddFilter(x => x.Order.Status == OrderStatus.Finished);
        AddFilter(x => x.ProductId == productId);
    }
}