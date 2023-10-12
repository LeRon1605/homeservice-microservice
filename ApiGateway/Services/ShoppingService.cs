using ApiGateway.Dtos.Shopping;
using ApiGateway.Services.Interfaces;
using Products.Application.Grpc.Proto;
using Shopping.Application.Grpc.Proto;

namespace ApiGateway.Services;

public class ShoppingService : IShoppingService
{
    private readonly ShoppingGrpcService.ShoppingGrpcServiceClient _shoppingGrpcService;

    public ShoppingService(ShoppingGrpcService.ShoppingGrpcServiceClient shoppingGrpcService)
    {
        _shoppingGrpcService = shoppingGrpcService;
    }

    public ShoppingData GetShoppingProductById(Guid id)
    {
        var response = _shoppingGrpcService.GetProductById(new ShoppingProductByIdRequest {Id = id.ToString()});
        return new ShoppingData
        {
            Id = new Guid(response.Id),
            Name = response.Name,
            AverageRating = response.Rating,
            OriginPrice = Helpers.DecimalValueHelper.ToDecimal(response.OriginPrice),
            DiscountPrice = Helpers.DecimalValueHelper.ToDecimal(response.DiscountPrice),
            NumberOfRating = response.NumberOfRating,
            NumberOfOrder = response.NumberOfOrder
        };
    } 
}
