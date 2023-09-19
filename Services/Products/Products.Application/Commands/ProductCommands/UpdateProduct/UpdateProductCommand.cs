using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.ProductCommands.UpdateProduct;

public class UpdateProductCommand : ICommand<GetProductDto>
{
    public Guid Id { get; set; }
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

    public UpdateProductCommand(Guid id, ProductUpdateDto productUpdateDto)
    {
        Id = id;
        ProductCode = productUpdateDto.ProductCode;
        Name = productUpdateDto.Name;
        Description = productUpdateDto.Description;
        IsObsolete = productUpdateDto.IsObsolete;
        Sell = productUpdateDto.Sell;
        Buy = productUpdateDto.Buy;
        TypeId = productUpdateDto.TypeId;
        GroupId = productUpdateDto.GroupId;
        BuyUnitId = productUpdateDto.BuyUnitId;
        SellUnitId = productUpdateDto.SellUnitId;
        Urls = productUpdateDto.Urls;
    }
}