using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommand : ICommand<GetProductDto>
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public Guid TypeId { get; set; }
    public Guid GroupId { get; set; }
    public Guid BuyUnitId { get; set; }
    public Guid SellUnitId { get; set; }
    public decimal? Buy { get; set; }
    public decimal? Sell { get; set; }
    public bool IsObsolete { get; set; }
    public string? Description { get; set; }
    public string[] Urls { get;  set; }
    public AddProductCommand(ProductCreateDto productCreateDto)
    {
        ProductCode = productCreateDto.ProductCode;
        Name = productCreateDto.Name;
        Description = productCreateDto.Description;
        IsObsolete = productCreateDto.IsObsolete;
        Sell = productCreateDto.Sell;
        Buy = productCreateDto.Buy;
        TypeId = productCreateDto.TypeId;
        GroupId = productCreateDto.GroupId;
        BuyUnitId = productCreateDto.BuyUnitId;
        SellUnitId = productCreateDto.SellUnitId;
        Urls = productCreateDto.Urls;
    }
}