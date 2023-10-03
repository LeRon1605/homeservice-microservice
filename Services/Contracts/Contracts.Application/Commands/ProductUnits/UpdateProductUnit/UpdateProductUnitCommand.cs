using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.ProductUnits.UpdateProductUnit;

public class UpdateProductUnitCommand : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public UpdateProductUnitCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}