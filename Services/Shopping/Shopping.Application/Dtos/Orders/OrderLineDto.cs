namespace Shopping.Application.Dtos.Orders;

public class OrderLineDto
{
    public Guid Id { get; set; }
    public ProductOrderLineDto Product { get; set; } = null!;
    public ProductUnitOrderLineDto ProductUnit { get; set; } = null!;
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal TotalCost { get; set; }
}

public class ProductOrderLineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ProductUnitOrderLineDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
}