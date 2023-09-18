using BuildingBlocks.Application.CQRS;
using Customers.Application.Dtos;

namespace Customers.API.Controllers;

public class AddCustomerCommand : CustomerCreateDto, ICommand<CustomerDto>
{
    public AddCustomerCommand(CustomerCreateDto dto)
    {
        Name = dto.Name;
        ContactName = dto.ContactName;
        Email = dto.Email;
        Address = dto.Address;
        City = dto.City;
        State = dto.State;
        PostalCode = dto.PostalCode;
        Phone = dto.Phone;
    }
}