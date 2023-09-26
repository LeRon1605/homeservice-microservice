using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.ProductCommands.UpdateProduct;

public class UpdateProductCommand : ProductUpdateDto, ICommand<GetProductDto>
{
    public Guid Id { get; set; }

    public UpdateProductCommand(Guid id, ProductUpdateDto productUpdateDto)
    {
        Id = id;
        ProductCode = productUpdateDto.ProductCode;
        Name = productUpdateDto.Name;
        Description = productUpdateDto.Description;
        IsObsolete = productUpdateDto.IsObsolete;
        SellPrice = productUpdateDto.SellPrice;
        BuyPrice = productUpdateDto.BuyPrice;
        TypeId = productUpdateDto.TypeId;
        GroupId = productUpdateDto.GroupId;
        BuyUnitId = productUpdateDto.BuyUnitId;
        SellUnitId = productUpdateDto.SellUnitId;
        Urls = productUpdateDto.Urls;
        Colors = productUpdateDto.Colors;
    }
}