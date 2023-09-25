﻿using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Specification;
using Grpc.Core;
using Microsoft.VisualBasic;
using Shopping.Application.Grpc.Proto;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Specifications;

namespace Shopping.Application.Grpc.Services;

public class ShoppingProductGrpcService : ShoppingGrpcService.ShoppingGrpcServiceBase
{
    private readonly IRepository<Product> _productRepository;

    public ShoppingProductGrpcService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<ProductListResponse> GetProducts(ProductFilterSorting request, ServerCallContext context)
    {
        var specification = GetSpecification(request);
        var products = await _productRepository.FindListAsync(specification);
        return MapToProductListResponse(products);
    }

    private static Specification<Product> GetSpecification(ProductFilterSorting productFilterSorting)
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

    private static string GetFieldOrderBy(ProductFilterSorting productFilterSorting)
    {
        string orderBy = string.Empty;

        switch (productFilterSorting.OrderBy)
        {
            case 1:
                orderBy = nameof(Product.Name);
                break;
            case 2:
                orderBy = nameof(Product.Price);
                break;
            case 3:
                orderBy = nameof(ProductReview.Rating);
                break;
        }

        return orderBy;
    }

    private static ProductListResponse MapToProductListResponse(IEnumerable<Product> products)
    {
        var response = new ProductListResponse();

        foreach (var product in products)
        {
            var productItemResponse = new ProductItemResponse()
            {
                Id = product.Id.ToString(),
                Rating = product.Reviews.Select(x => x.Rating).DefaultIfEmpty(0).Average(),
                Name = product.Name,
                OriginPrice = DecimalValueHelper.ToDecimalValue(product.Price),
                DiscountPrice = DecimalValueHelper.ToDecimalValue(0),
                NumberOfRating = product.Reviews.Count(),
                NumberOfOrder = 0
            };
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