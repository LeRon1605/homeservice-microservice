using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands.DeleteProduct;

public class DeletedProductCommand : ICommand
{
    public Guid Id { get; set; }
    // public string Name { get; private set; }
    // public Guid ProductTypeId { get; private set; }
    // public decimal? SellPrice { get; private set; }

    public DeletedProductCommand(Guid id)
    {
        Id = id;
        // Name = name;
        // ProductTypeId = productTypeId;
        // SellPrice = sellPrice;
    }
}