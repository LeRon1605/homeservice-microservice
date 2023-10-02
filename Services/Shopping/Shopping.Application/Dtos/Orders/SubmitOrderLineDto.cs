namespace Shopping.Application.Dtos.Orders;

public class SubmitOrderLineDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Color { get; set; }
}