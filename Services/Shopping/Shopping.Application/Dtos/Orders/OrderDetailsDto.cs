using System.ComponentModel.DataAnnotations;

namespace Shopping.Application.Dtos.Orders;

public class OrderDetailsDto
{
    [Required] public string CustomerName { get; private set; }
    [Required] public string ContactName { get; private set; }
    public string? Email { get; private set; }
    [Required] public string Phone { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? PostalCode { get; private set; }
    
    public List<OrderLineDto>? Items { get; private set; }
}