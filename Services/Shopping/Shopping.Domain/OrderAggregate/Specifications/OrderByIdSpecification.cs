using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.OrderAggregate.Specifications;

public class OrderByIdSpecification : Specification<Order>
{
    public OrderByIdSpecification(Guid id)
    {
        AddFilter(x=>x.Id == id);
        AddInclude(x=>x.OrderLines);
    }
}