using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.ShoppingAggregate;

public class Order: AggregateRoot
{
    public string Name { get;  set; }
}