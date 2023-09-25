using ApiGateway.Dtos.Products;
using ApiGateway.Services.Interfaces;
using BuildingBlocks.Application.Dtos;
using Products.Application.Grpc.Proto;

namespace ApiGateway.Services;

public class ProductService : IProductService
{
    private readonly ProductGrpcService.ProductGrpcServiceClient _productGrpcService;
    
    public ProductService(ProductGrpcService.ProductGrpcServiceClient productGrpcService)
    {
        _productGrpcService = productGrpcService;
    }
    
    public async Task<PagedResult<ProductData>> GetPagedAsync()
    {
        var productIds = new ProductIds();
        productIds.Id.Add("18cef494-534e-42d7-84e9-dde9ac48235b");
        
        var responseInProductService = _productGrpcService.GetProducts(productIds);
        var result = new List<ProductData>();
        
        foreach (var productItemResponse in responseInProductService.Products)
        {
            result.Add(MapToProductData(productItemResponse));
        }

        return new PagedResult<ProductData>(result, 1,1, 10);
    }

    private ProductData MapToProductData(ProductItemResponse productItemResponse)
    {
        return new ProductData()
        {
            Id = new Guid(productItemResponse.Id),
            Name = productItemResponse.Name,
            Description = productItemResponse.Description,
            ProductCode = productItemResponse.Code,
            BuyPrice = DecimalValueHelper.ToDecimal(productItemResponse.BuyPrice),
            SellPrice = DecimalValueHelper.ToDecimal(productItemResponse.SellPrice),
            SellUnit = MapToProductUnitDto(productItemResponse.SellUnit),
            BuyUnit = MapToProductUnitDto(productItemResponse.BuyUnit),
            IsObsolete = productItemResponse.IsObsolete,
            Type = new ProductTypeDto()
            {
                Id = new Guid(productItemResponse.ProductType.Id),
                Name = productItemResponse.ProductType.Name
            },
            Group = new ProductGroupDto()
            {
                Id = new Guid(productItemResponse.ProductGroup.Id),
                Name = productItemResponse.ProductGroup.Name
            },
            Images = productItemResponse.Images.Select(x => new ProductImageDto()
            {
                Id = new Guid(x.Id),
                Url = x.Url
            }),
            AverageRating = 0
        };
    }

    private ProductUnitDto? MapToProductUnitDto(ProductUnitResponse sellUnit)
    {
        return sellUnit.Id == null
            ? null
            : new ProductUnitDto()
            {
                Id = new Guid(sellUnit.Id),
                Name = sellUnit.Name
            };
    }
}

public static class DecimalValueHelper
{
    private const decimal NanoFactor = 1_000_000_000;
    
    public static decimal ToDecimal(DecimalValue grpcDecimal)
    {
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
    
    public static decimal? ToDecimal(NullableDecimalValue grpcDecimal)
    {
        if (!grpcDecimal.Units.HasValue || !grpcDecimal.Nanos.HasValue)
        {
            return null;
        }
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }

    public static NullableDecimalValue ToDecimalValue(decimal? value)
    {
        if (!value.HasValue)
        {
            return new NullableDecimalValue()
            {
                Units = null,
                Nanos = null
            };
        }
        var units = decimal.ToInt64(value.Value);
        var nanos = decimal.ToInt32((value.Value - units) * NanoFactor);
        return new NullableDecimalValue()
        {
            Units = units,
            Nanos = nanos
        };
    }
    
    public static DecimalValue ToDecimalValue(decimal value)
    {
        var units = decimal.ToInt64(value);
        var nanos = decimal.ToInt32((value - units) * NanoFactor);
        return new DecimalValue()
        {
            Units = units,
            Nanos = nanos
        };
    }
}