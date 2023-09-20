using BuildingBlocks.Application.CQRS;

namespace Products.Application.Commands.MaterialCommands.DeleteMaterial;

public class DeleteMaterialCommand : ICommand
{
    public Guid Id { get; set; }
    
    public DeleteMaterialCommand(Guid id)
    {
        Id = id;
    }
}