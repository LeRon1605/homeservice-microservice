using BuildingBlocks.Application.CQRS;

namespace Products.Application.Commands.ProductCommands.DeleteProduct;

public class DeleteProductCommand : ICommand
{
    public Guid Id { get; set; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}