using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Specification;
using Google.Protobuf.Collections;
using Grpc.Core;
using Products.Application.Grpc.Proto;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Grpc.Services;

public class ProductGrpcService : Proto.ProductGrpcService.ProductGrpcServiceBase
{
    private readonly IReadOnlyRepository<Product> _productRepository;
    
    public ProductGrpcService(IReadOnlyRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public override async Task<ProductListResponse> GetProducts(ProductIds request, ServerCallContext context)
    {
        var specification = GetSpecification(request.Id);
        var products = await _productRepository.FindListAsync(specification);
    
        return MapToProductListResponse(products);
    }
    
    private Specification<Product> GetSpecification(RepeatedField<string> ids)
    {
        try
        {
            var specification = new ProductByIncludedIdsSpecification(ids.Distinct().Select(x => Guid.Parse(x)).ToArray());
            return specification;
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Id!"));
        }
    }
    
    private ProductListResponse MapToProductListResponse(IEnumerable<Product> products)
    {
        var response = new ProductListResponse();
    
        foreach (var product in products)
        {
            var productItemResponse = new ProductItemResponse()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                IsObsolete = product.IsObsolete,
                Code = product.ProductCode,
                SellUnit = new ProductUnitResponse()
                {
                    Id = product.SellUnit?.Id.ToString(),
                    Name = product.SellUnit?.Name
                },
                BuyUnit = new ProductUnitResponse()
                {
                    Id = product.BuyUnit?.Id.ToString(),
                    Name = product.BuyUnit?.Name
                },
                SellPrice = DecimalValueHelper.ToDecimalValue(product.SellPrice),
                BuyPrice = DecimalValueHelper.ToDecimalValue(product.BuyPrice.Value),
                ProductGroup = new ProductGroupResponse()
                {
                    Id = product.Group.Id.ToString(),
                    Name = product.Group.Name
                },
                ProductType = new ProductTypeResponse()
                {
                    Id = product.Type.Id.ToString(),
                    Name = product.Type.Name
                }
            };
    
            foreach (var image in product.Images)
            {
                productItemResponse.Images.Add(new ProductImageReponse()
                {
                    Id = image.Id.ToString(),
                    Url = image.Url
                });
            }
            response.Products.Add(productItemResponse);
        }
        
        return response;
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