using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos;

public class OrderDto
{
    public Guid Id { get; private set; }
    public string OrderNo { get; private set; }
    public string ContactName { get; private set; }
    public Guid BuyerId { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? EmailAddress { get; private set; }
    public decimal OrderValue { get; private set; }
    public DateTime PlacedDate { get; private set; }
    public OrderStatus Status { get; private set; }

}