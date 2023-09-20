using BuildingBlocks.Application.CQRS;
using Customers.Application.Dtos;

namespace Customers.Application.Commands.EditCustomer;

public class EditCustomerCommand : ICommand<CustomerDto>
{
    public Guid Id { get; init; }
    
    public string? Name { get; set; }
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    
    public EditCustomerCommand(Guid id,
                               string name,
                               string? contactName,
                               string? email,
                               string? address,
                               string? city,
                               string? state,
                               string? postalCode,
                               string? phone)
    {
        Id = id;
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