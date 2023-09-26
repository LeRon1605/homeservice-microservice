using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Specification;
using Grpc.Core;
using Shopping.Application.Grpc.Proto;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Specifications;
using DecimalValue = Shopping.Application.Grpc.Proto.DecimalValue;
using NullableDecimalValue = Shopping.Application.Grpc.Proto.NullableDecimalValue;

namespace Shopping.Application.Grpc.Services;

public class ShoppingProductGrpcService : ShoppingGrpcService.ShoppingGrpcServiceBase
{
    private readonly IReadOnlyRepository<Product> _productRepository;
    
    public ShoppingProductGrpcService(IReadOnlyRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public override async Task<ShoppingProductListResponse> GetProducts(ShoppingProductFilterSorting request, ServerCallContext context)
    {
        var specification = GetSpecification(request);
        
        var (products, total) = await _productRepository.FindWithTotalCountAsync(specification);
        return MapToShoppingProductListResponse(products, total);
    }
    
    public override async Task<ShoppingProductItemResponse> GetProductById(ShoppingProductByIdRequest request, ServerCallContext context)
    {
        var specification = new ProductByIdSpecification(Guid.Parse(request.Id));
        var product = await _productRepository.FindAsync(specification)
                      ?? throw new RpcException(new Status(StatusCode.NotFound, $"Product ({request.Id}) not found!"));
        // var product = new Product(Guid.NewGuid(), "Temp", Guid.NewGuid(), 0);
        return MapToShoppingProductItemResponse(product);
    }

    private static Specification<Product> GetSpecification(ShoppingProductFilterSorting productFilterSorting)
    {
        try
        {
            var specification = new ProductFilterSortingSpecification(
                string.IsNullOrWhiteSpace(productFilterSorting.ProductGroupId.Strings) ? null : Guid.Parse(productFilterSorting.ProductGroupId.Strings),
                DecimalValueHelper.ToDecimal(productFilterSorting.MinPrice),
                DecimalValueHelper.ToDecimal(productFilterSorting.MaxPrice),
                productFilterSorting.Rating,
                productFilterSorting.IsDescending,
                GetFieldOrderBy(productFilterSorting),
                productFilterSorting.PageIndex,
                productFilterSorting.PageSize,
                productFilterSorting.Search.Strings);
            return specification;
        }
        catch (FormatException e)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid guid id!"));
        }
    }
    
    private static string GetFieldOrderBy(ShoppingProductFilterSorting productFilterSorting)
    {
        var orderBy = string.Empty;
    
        switch (productFilterSorting.OrderBy)
        {
            case 0:
                orderBy = nameof(Product.Name);
                break;
            case 1:
                orderBy = nameof(Product.Price);
                break;
            case 2:
                orderBy = $"Reviews.Average(Rating)";
                break;
        }
    
        return orderBy;
    }
    
    private static ShoppingProductListResponse MapToShoppingProductListResponse(IEnumerable<Product> products, int total)
    {
        var response = new ShoppingProductListResponse()
        {
            TotalCount = total
        };
    
        foreach (var product in products)
        {
            var productItemResponse = MapToShoppingProductItemResponse(product);
            response.Products.Add(productItemResponse);
        }
    
        return response;
    }

    private static ShoppingProductItemResponse MapToShoppingProductItemResponse(Product product)
    {
        return new ShoppingProductItemResponse
        {
            Id = product.Id.ToString(),
            Rating = product.Reviews.Select(x => x.Rating).DefaultIfEmpty(0).Average(),
            Name = product.Name,
            OriginPrice = DecimalValueHelper.ToDecimalValue(product.Price),
            DiscountPrice = DecimalValueHelper.ToDecimalValue(null),
            NumberOfRating = product.Reviews.Count,
            NumberOfOrder = 0
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