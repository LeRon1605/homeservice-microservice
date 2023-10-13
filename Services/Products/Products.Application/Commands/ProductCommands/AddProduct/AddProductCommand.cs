using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommand : ProductCreateDto, ICommand<GetProductDto>
{
    public AddProductCommand(ProductCreateDto productCreateDto)
    {
        ProductCode = productCreateDto.ProductCode;
        Name = productCreateDto.Name;
        Description = productCreateDto.Description;
        IsObsolete = productCreateDto.IsObsolete;
        SellPrice = productCreateDto.SellPrice;
        BuyPrice = productCreateDto.BuyPrice;
        TypeId = productCreateDto.TypeId;
        GroupId = productCreateDto.GroupId;
        BuyUnitId = productCreateDto.BuyUnitId;
        SellUnitId = productCreateDto.SellUnitId;
        Urls = productCreateDto.Urls;
        Colors = productCreateDto.Colors;
    }
}