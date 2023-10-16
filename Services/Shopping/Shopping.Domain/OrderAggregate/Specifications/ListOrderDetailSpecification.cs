using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class ListOrderDetailSpecification : Specification<Order>
{
    public ListOrderDetailSpecification()
    {
        AddInclude(x=> x.OrderLines);
    }
}