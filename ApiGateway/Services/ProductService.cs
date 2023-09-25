using ApiGateway.Dtos.Products;
using ApiGateway.Services.Interfaces;
using BuildingBlocks.Application.Dtos;
using Products.Application.Grpc.Proto;
using Shopping.Application.Grpc.Proto;

namespace ApiGateway.Services;

public class ProductService : IProductService
{
    private readonly ProductGrpcService.ProductGrpcServiceClient _productGrpcService;
    private readonly ShoppingGrpcService.ShoppingGrpcServiceClient _shoppingGrpcService;
    
    public ProductService(
        ProductGrpcService.ProductGrpcServiceClient productGrpcService,
        ShoppingGrpcService.ShoppingGrpcServiceClient shoppingGrpcService)
    {
        _productGrpcService = productGrpcService;
        _shoppingGrpcService = shoppingGrpcService;
    }
    
    public Task<PagedResult<ProductData>> GetPagedAsync(GetProductWithFilterAndPaginationDto dto)
    {
        var responseInShoppingService = _shoppingGrpcService.GetProducts(GetFilterSortingRequestParam(dto));
        var responseInProductService = GetIncludedProductsInProductService(responseInShoppingService);
        
        var result = new List<ProductData>();
        foreach (var productInShoppingService in  responseInShoppingService.Products)
        {
            var productInProductService = responseInProductService.Products.FirstOrDefault(x => x.Id == productInShoppingService.Id);
            if (productInProductService == null)
            {
                continue;
            }
            
            result.Add(MapToProductData(productInProductService, productInShoppingService));
        }

        return Task.FromResult(new PagedResult<ProductData>(result, responseInShoppingService.TotalCount, dto.PageIndex, dto.PageSize));
    }

    private ShoppingProductFilterSorting GetFilterSortingRequestParam(GetProductWithFilterAndPaginationDto dto)
    {
        var request = new ShoppingProductFilterSorting()
        {
            PageIndex = dto.PageIndex,
            PageSize = dto.PageSize,
            Rating = dto.Rating,
            ProductGroupId = new NullableStringValue()
            {
                Strings = dto.GroupId.HasValue ? dto.GroupId.Value.ToString() : string.Empty
            },
            Search = new NullableStringValue()
            {
                Strings = string.IsNullOrWhiteSpace(dto.Search) ? string.Empty : dto.Search
            },
            MinPrice = DecimalValueHelper.ToDecimalValue(dto.MinPrice),
            MaxPrice = DecimalValueHelper.ToDecimalValue(dto.MaxPrice),
        };

        if (dto.OrderBy.HasValue)
        {
            request.OrderBy = (int)dto.OrderBy.Value;
            request.IsDescending = dto.IsDescending;
        }

        return request;
    }

    private ProductListResponse GetIncludedProductsInProductService(ShoppingProductListResponse responseInShoppingService)
    {
        var productIds = new ProductIds();
        foreach (var item in responseInShoppingService.Products)
        {
            productIds.Id.Add(item.Id);
        }
        
        return _productGrpcService.GetProducts(productIds);
    }

    private ProductData MapToProductData(ProductItemResponse productItemResponse, ShoppingProductItemResponse item)
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
            AverageRating = item.Rating,
            NumberOfRating = item.NumberOfRating,
            DiscountPrice = DecimalValueHelper.ToDecimal(item.DiscountPrice),
            NumberOfOrder = item.NumberOfOrder
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

    public static decimal ToDecimal(Products.Application.Grpc.Proto.DecimalValue grpcDecimal)
    {
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
    
    public static decimal? ToDecimal(Products.Application.Grpc.Proto.NullableDecimalValue grpcDecimal)
    {
        if (!grpcDecimal.Units.HasValue || !grpcDecimal.Nanos.HasValue)
        {
            return null;
        }
        
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
    
    public static decimal? ToDecimal(Shopping.Application.Grpc.Proto.NullableDecimalValue grpcDecimal)
    {
        if (!grpcDecimal.Units.HasValue || !grpcDecimal.Nanos.HasValue)
        {
            return null;
        }
        
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }

    public static Shopping.Application.Grpc.Proto.NullableDecimalValue ToDecimalValue(decimal? value)
    {
        if (!value.HasValue)
        {
            return new Shopping.Application.Grpc.Proto.NullableDecimalValue()
            {
                Units = null,
                Nanos = null
            };
        }
        var units = decimal.ToInt64(value.Value);
        var nanos = decimal.ToInt32((value.Value - units) * NanoFactor);
        return new Shopping.Application.Grpc.Proto.NullableDecimalValue()
        {
            Units = units,
            Nanos = nanos
        };
    }
}