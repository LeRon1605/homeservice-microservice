using BuildingBlocks.Application.CQRS;

namespace Customers.Application.Commands.AddSignedUpCustomer;

public class AddSignedUpCustomerCommand : ICommand
{
    public Guid Id { get; }
    public string FullName { get; }
    public string? Email { get; }
    public string Phone { get; }

    public AddSignedUpCustomerCommand(Guid id,
                                      string fullName,
                                      string? email,
                                      string phone)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
    } 
}