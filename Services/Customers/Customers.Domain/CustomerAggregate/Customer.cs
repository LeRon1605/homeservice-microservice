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
        string? name = null,
        string? contactName = null,
        string? email = null,
        string? address = null,
        string? city = null,
        string? state = null,
        string? postalCode = null,
        string? phone = null)
    {
        Name = name ?? "Retail Customer";
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = new CustomerAddress(address, city, state, postalCode);
    }

    private Customer()
    {
    }
    
    public void Update(string? name = null,
                       string? contactName = null,
                       string? email = null,
                       string? address = null,
                       string? city = null,
                       string? state = null,
                       string? postalCode = null,
                       string? phone = null)
    {
        Name = name ?? "Retail Customer";
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = new CustomerAddress(address, city, state, postalCode);
    }
    
    public void Delete()
    {
        // TODO:
        // Add domain event
        // Check constraints? (customer has no orders, etc.)
    }
    
    public Customer CreateWithId(
                           Guid id,
                           string? name = "Retail Customer",
                           string? contactName = null,
                           string? email = null,
                           string? address = null,
                           string? city = null,
                           string? state = null,
                           string? postalCode = null,
                           string? phone = null)
    {
        var customer = new Customer(name, contactName, email, address, city, state, postalCode, phone)
        {
            Id = id
        };
        return customer;
    }
}