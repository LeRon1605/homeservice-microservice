using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands.AddSignedUpUser;

public class AddSignedUpUserCommand : ICommand
{
    public Guid Id { get; }
    public string FullName { get; }
    public string? Email { get; }
    public string Phone { get; }

    public AddSignedUpUserCommand(Guid id,
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