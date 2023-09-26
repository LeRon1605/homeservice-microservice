using ApiGateway.Dtos.Shopping;

namespace ApiGateway.Services.Interfaces;

public interface IShoppingService
{
    ShoppingData GetShoppingProductById(Guid id);
}