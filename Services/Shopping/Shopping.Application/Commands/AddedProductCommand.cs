using BuildingBlocks.Application.CQRS;

namespace Shopping.Application.Commands;

public class AddedProductCommand : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public decimal SellPrice { get; private set; }
    
    public AddedProductCommand(Guid id, string name, Guid productGroupId, decimal sellPrice)
    {
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
        SellPrice = sellPrice;
    }
}