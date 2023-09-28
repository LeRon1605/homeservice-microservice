using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands;

public class DeleteProductUnitCommand : ICommand
{
    public Guid Id { get; set; }
    
    public DeleteProductUnitCommand(Guid id)
    {
        Id = id;
    }
}