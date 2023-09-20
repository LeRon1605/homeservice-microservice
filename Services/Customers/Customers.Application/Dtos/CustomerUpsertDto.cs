using System.ComponentModel.DataAnnotations;

namespace Customers.Application.Dtos;

public class CustomerUpsertDto
{
    public string Name { get; set; } = null!;
    public string? ContactName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    [MaxLength(6)]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Postal code must be numeric.")]
    public string? PostalCode { get; set; }
    [MaxLength(11)]
    [RegularExpression("^0[0-9]*$", ErrorMessage = "Phone number must be numeric and start with 0.")]
    public string? Phone { get; set; }
}