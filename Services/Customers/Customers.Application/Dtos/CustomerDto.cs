namespace Customers.Application.Dtos;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
}