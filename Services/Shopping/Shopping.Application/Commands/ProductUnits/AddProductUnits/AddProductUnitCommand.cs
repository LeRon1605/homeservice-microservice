using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands.ProductUnits.AddProductUnits;

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