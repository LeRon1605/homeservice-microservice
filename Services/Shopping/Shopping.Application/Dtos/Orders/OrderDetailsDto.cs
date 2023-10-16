using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos.Orders;

public class OrderDetailsDto
{
    public Guid Id { get; set; }
    public int OrderNo { get; set; }
    public Guid BuyerId { get; set; }
    public DateTime PlacedDate { get; set; }
    public string CustomerName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }

    public List<OrderLineDto> Items { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; }
}