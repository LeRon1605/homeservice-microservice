namespace Shopping.Application.Dtos.Orders;

public class OrderSubmitDto
{
    public IEnumerable<OrderLineDto> Items { get; set; } = null!;
}