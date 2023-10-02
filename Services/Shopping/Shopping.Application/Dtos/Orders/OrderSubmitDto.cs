namespace Shopping.Application.Dtos.Orders;

public class OrderSubmitDto
{
    public IEnumerable<SubmitOrderLineDto> Items { get; set; } = null!;
}