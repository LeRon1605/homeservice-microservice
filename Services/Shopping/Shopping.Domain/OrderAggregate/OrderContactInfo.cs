using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Shopping.Domain.OrderAggregate;

public record OrderContactInfo : ValueObject
{
    public string CustomerName { get; private set; }
    public string ContactName { get; private set; }
    public string? Email { get; private set; }
    public string Phone { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? PostalCode { get; private set; }
    
    public OrderContactInfo(
        string customerName, 
        string contactName, 
        string? email, 
        string phone, 
        string? address, 
        string? city, 
        string? state, 
        string? postalCode)
    {
        CustomerName = Guard.Against.NullOrWhiteSpace(customerName, nameof(CustomerName));
        ContactName = Guard.Against.NullOrWhiteSpace(contactName, nameof(ContactName));
        Email = Guard.Against.NullOrWhiteSpace(email, nameof(Email));
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        PostalCode = postalCode;
    }
}