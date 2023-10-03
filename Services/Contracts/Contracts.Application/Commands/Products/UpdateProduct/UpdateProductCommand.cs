using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Products.UpdateProduct;

public class UpdateProductCommand : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public decimal SellPrice { get; private set; }
    public string? Colors { get; private set; }

    public UpdateProductCommand(Guid id, string name, Guid productGroupId, Guid? productUnitId, decimal sellPrice, string? colors)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
        Colors = colors;
    }
}