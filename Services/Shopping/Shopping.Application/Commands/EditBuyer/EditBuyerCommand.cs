using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos;
using Shopping.Application.Dtos.Buyers;

namespace Shopping.Application.Commands.EditBuyer;

public class EditBuyerCommand : ICommand<BuyerDto>
{
    public Guid Id { get; set; }
    
    public string Name { get; init; }
    public string FullName { get; init; }
    public string? Email { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? PostalCode { get; init; }
    public string Phone { get; init; }
    public string? AvatarUrl { get; init; }
    
    public EditBuyerCommand(string fullName,
                            string? email,
                            string? address,
                            string phone,
                            string? city,
                            string? state,
                            string? postalCode,
                            string? avatarUrl)
    {
        FullName = fullName;
        Email = email;
        Address = address;
        City = city;
        State = state;
        PostalCode = postalCode;
        Phone = phone;
        AvatarUrl = avatarUrl;
    }
}