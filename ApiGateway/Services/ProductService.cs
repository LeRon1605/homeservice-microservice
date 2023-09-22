using ApiGateway.Services.Interfaces;
using Products.Application.Grpc.Proto;

namespace ApiGateway.Services;

public class ProductService : IProductService
{
    private readonly ProductGrpcService.ProductGrpcServiceClient _productGrpcService;
    
    public ProductService(ProductGrpcService.ProductGrpcServiceClient productGrpcService)
    {
        _productGrpcService = productGrpcService;
    }
    
    public async Task GetPagedAsync()
    {
        var productIds = new ProductIds();
        productIds.Id.Add("18cef494-534e-42d7-84e9-dde9ac48235b");
        var data = _productGrpcService.GetProducts(productIds);
        
        
    }
}