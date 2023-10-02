using System.ComponentModel.DataAnnotations;

namespace Shopping.Application.Dtos.Orders;

public class OrderLineDto
{
    [Required] public string ProductName { get; private set; }
    public string? UnitName { get; private set; }
    public string? Color { get; private set; }
    public int Quantity { get; private set; }
    public decimal Cost { get; private set; }

    public decimal TotalCost { get; private set; }
}