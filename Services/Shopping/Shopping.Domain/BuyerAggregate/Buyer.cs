using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Shopping.Domain.BuyerAggregate.ValueObjects;

namespace Shopping.Domain.BuyerAggregate;

public class Buyer : AggregateRoot
{
    public string FullName { get; private set; }
    public string? Email { get; private set; }
    public string Phone { get; private set; }
    public BuyerAddress Address { get; private set; }
    public string? AvatarUrl { get; private set; }

    public Buyer(
        string? fullName,
        string? phone,
        string? email = null,
        string? address = null,
        string? city = null,
        string? state = null,
        string? postalCode = null,
        string? avatarUrl = null)
    {
        FullName = Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
        Phone = Guard.Against.NullOrWhiteSpace(phone, nameof(phone));
        Email = email;
        Address = new BuyerAddress(address, city, state, postalCode);
        AvatarUrl = avatarUrl;
    }

    private Buyer()
    {
    }
    
    public void Update(string? fullName,
                       string? phone,
                       string? email = null,
                       string? address = null,
                       string? city = null,
                       string? state = null,
                       string? postalCode = null,
                       string? avartarUrl = null)
    {
        FullName = Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
        Phone = Guard.Against.NullOrWhiteSpace(phone, nameof(phone));
        Email = email;
        Address = new BuyerAddress(address, city, state, postalCode);
        AvatarUrl = avartarUrl;
    }
    
    public void Delete()
    {
        // TODO:
        // Add domain event
        // Check constraints? (customer has no orders, etc.)
    }
    
    public static Buyer CreateWithId(
                           Guid id,
                           string? fullName,
                           string? phone,
                           string? email = null,
                           string? address = null,
                           string? city = null,
                           string? state = null,
                           string? postalCode = null)
    {
        var buyer = new Buyer(fullName, phone, email, address, city, state, postalCode)
        {
            Id = id
        };
        return buyer;
    }
}