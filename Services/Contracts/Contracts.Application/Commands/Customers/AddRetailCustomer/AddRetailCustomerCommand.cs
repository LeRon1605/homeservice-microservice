using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Customers.AddRetailCustomer;

public class AddRetailCustomerCommand : ICommand
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string Phone { get; set; }
    
    public AddRetailCustomerCommand(
                              Guid id,
                              string fullName,
                              string? email,
                              string phone,
                              string? address,
                              string? city,
                              string? state,
                              string? postalCode)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Address = address;
        City = city;
        State = state;
        PostalCode = postalCode;
        Phone = phone;
    }
}