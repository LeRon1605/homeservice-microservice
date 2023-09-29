using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands.Products.AddProduct;

public class AddProductCommand : ICommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public decimal SellPrice { get; private set; }

    public AddProductCommand(Guid id, string name, Guid productGroupId, decimal sellPrice,  Guid? productUnitId)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
    }
}