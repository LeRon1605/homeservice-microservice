using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands;

public class AddedProductCommand : ICommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public decimal SellPrice { get; private set; }

    public AddedProductCommand(Guid id, string name, Guid productGroupId, Guid? productUnitId, decimal sellPrice)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
    }
}