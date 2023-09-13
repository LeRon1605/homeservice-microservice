using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommand : ICommand<GetProductDto>
{
    public string Name { get; set; } = string.Empty; 
}