using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public string OrderNo { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public Guid BuyerId { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? EmailAddress { get; set; }
    public decimal OrderValue { get; set; }
    public DateTime PlacedDate { get; set; }
    public OrderStatus Status { get; set; }

}