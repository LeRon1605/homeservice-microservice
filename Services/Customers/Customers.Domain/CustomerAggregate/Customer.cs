using BuildingBlocks.Domain.Models;

namespace Customers.Domain.CustomerAggregate;

public class Customer : AggregateRoot
{
    public string CustomerName { get; private set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    
}