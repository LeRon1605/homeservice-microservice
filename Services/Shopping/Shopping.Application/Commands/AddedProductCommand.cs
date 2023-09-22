using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands;

public class AddedProductCommand : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductTypeId { get; private set; }
    public decimal? SellPrice { get; private set; }
    public AddedProductCommand(Guid id, string name, Guid productTypeId, decimal? sellPrice)
    {
        Id = id;
        Name = name;
        ProductTypeId = productTypeId;
        SellPrice = sellPrice;
    }
}