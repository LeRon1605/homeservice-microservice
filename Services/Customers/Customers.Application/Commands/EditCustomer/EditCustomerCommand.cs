using BuildingBlocks.Application.CQRS;
using Customers.Application.Dtos;

namespace Customers.Application.Commands.EditCustomer;

public class EditCustomerCommand : ICommand<CustomerDto>
{
    public Guid Id { get; set; }
    
    public string? Name { get; init; }
    public string? ContactName { get; init; }
    public string? Email { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? PostalCode { get; init; }
    public string? Phone { get; init; }
    
    public EditCustomerCommand(string name,
                               string? contactName,
                               string? email,
                               string? address,
                               string? city,
                               string? state,
                               string? postalCode,
                               string? phone)
    {
        Name = name;
        ContactName = contactName;
        Email = email;
        Address = address;
        City = city;
        State = state;
        PostalCode = postalCode;
        Phone = phone;
    }
}