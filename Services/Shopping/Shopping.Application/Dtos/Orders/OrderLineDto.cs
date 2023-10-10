using System.ComponentModel.DataAnnotations;

namespace Shopping.Application.Dtos.Orders;

public class OrderLineDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public Guid? ProductUnitId { get; set; }
    public string? UnitName { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal TotalCost { get; set; }
}