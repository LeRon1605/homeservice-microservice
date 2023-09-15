using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Customers.Domain.CustomerAggregate.ValueObjects;

namespace Customers.Domain.CustomerAggregate;

public class Customer : AggregateRoot
{
    public string Name { get; private set; }
    public string? ContactName { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public CustomerAddress Address { get; private set; }

    public Customer(
        string name,
        string? contactName = null,
        string? email = null,
        string? address = null,
        string? city = null,
        string? state = null,
        string? postalCode = null,
        string? phone = null)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ContactName = contactName;
        Email = email;
        Address = new CustomerAddress(address, city, state, postalCode);
        Phone = phone;
    }

    private Customer()
    {
        
    }
}