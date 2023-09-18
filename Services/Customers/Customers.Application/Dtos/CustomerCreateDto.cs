using System.ComponentModel.DataAnnotations;

namespace Customers.Application.Dtos;

public class CustomerCreateDto
{
    public string Name { get; set; } = null!;
    public string? ContactName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    [MaxLength(6)]
    [RegularExpression("^[0-9]*$")]
    public string? PostalCode { get; set; }
    [MaxLength(11)]
    [RegularExpression("^0[0-9]*$")]
    public string? Phone { get; set; }
}