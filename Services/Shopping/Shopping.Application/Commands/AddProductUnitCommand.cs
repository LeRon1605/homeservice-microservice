using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands;

public class AddProductUnitCommand : ICommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public AddProductUnitCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}