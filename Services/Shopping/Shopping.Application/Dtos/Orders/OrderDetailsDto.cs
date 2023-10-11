using System.ComponentModel.DataAnnotations;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos.Orders;

public class OrderDetailsDto
{
    public string CustomerName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }

    public List<OrderLineDto>? Items { get; set; }
    public OrderStatus OrderStatus { get; set; }
}