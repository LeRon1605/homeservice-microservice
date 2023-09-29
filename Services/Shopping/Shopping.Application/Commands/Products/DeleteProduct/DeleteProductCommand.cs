using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands.Products.DeleteProduct;

public class DeleteProductCommand : ICommand
{
    public Guid Id { get; set; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}