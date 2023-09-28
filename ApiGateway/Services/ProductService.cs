using System.Net;
using ApiGateway.Dtos.Products;
using ApiGateway.Exceptions;
using ApiGateway.Services.Interfaces;
using AutoMapper;
using BuildingBlocks.Application.Dtos;
using Products.Application.Grpc.Proto;
using Shopping.Application.Grpc.Proto;

namespace ApiGateway.Services;

public class ProductService : IProductService
{
    private readonly ProductGrpcService.ProductGrpcServiceClient _productGrpcService;
    private readonly ShoppingGrpcService.ShoppingGrpcServiceClient _shoppingGrpcService;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    
    public ProductService(
        ProductGrpcService.ProductGrpcServiceClient productGrpcService,
        ShoppingGrpcService.ShoppingGrpcServiceClient shoppingGrpcService,
        HttpClient httpClient,
        IMapper mapper)
    {
        _productGrpcService = productGrpcService;
        _shoppingGrpcService = shoppingGrpcService;
        _httpClient = httpClient;
        _mapper = mapper;
    }
    
    public PagedResult<ProductData> GetPaged(GetProductWithFilterAndPaginationDto dto)
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

        return new PagedResult<ProductData>(result, responseInShoppingService.TotalCount, dto.PageIndex, dto.PageSize);
    }
    
    public async Task<ProductData> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync(id.ToString());
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpClientException(response.StatusCode, response.ReasonPhrase);
        } 

        var productDto = await response.Content.ReadFromJsonAsync<GetProductDto>();

        return _mapper.Map<ProductData>(productDto);
    }

    public async Task<IEnumerable<ProductData>> GetProductByIncludedIdsAsync(IEnumerable<Guid> ids)
    {
        ids = ids.Distinct();
        var idsShoppingRequest = new ShoppingProductByIdsRequest();
        var idsProductRequest = new ProductIds(); 
        
        foreach (var id in ids)
        {
            idsShoppingRequest.Id.Add(id.ToString());
            idsProductRequest.Id.Add(id.ToString());
        }

        var productsShoppingService = await _shoppingGrpcService.GetProductsByIncludedIdsAsync(idsShoppingRequest);
        var productsProductService = await _productGrpcService.GetProductsAsync(idsProductRequest);
        
        if (productsShoppingService.Products.Count == productsProductService.Products.Count &&
            ids.Count() != productsProductService.Products.Count)
        {
            var notFoundProductId = ids.FirstOrDefault(x => productsProductService.Products.Select(p => p.Id).Contains(x.ToString()));
            throw new HttpClientException(HttpStatusCode.NotFound, $"Product with id '{notFoundProductId}' does not exist");
        }

        var result = new List<ProductData>();
        foreach (var productInShoppingService in  productsShoppingService.Products)
        {
            var productInProductService = productsProductService.Products.FirstOrDefault(x => x.Id == productInShoppingService.Id);
            if (productInProductService == null)
            {
                continue;
            }
            
            result.Add(MapToProductData(productInProductService, productInShoppingService));
        }

        return result;
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
            MinPrice = Helpers.DecimalValueHelper.ToDecimalValue(dto.MinPrice),
            MaxPrice = Helpers.DecimalValueHelper.ToDecimalValue(dto.MaxPrice),
        };

        if (dto.OrderBy.HasValue)
        {
            request.OrderBy = (int)dto.OrderBy.Value;
            request.IsDescending = dto.IsDescending;
        }

        return request;
    }

    private ProductListResponse GetIncludedProductsInProductService(ShoppingProductPagedResponse responseInShoppingService)
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
            SellPrice = Helpers.DecimalValueHelper.ToDecimal(productItemResponse.SellPrice),
            Group = new ProductGroupDto()
            {
                Id = new Guid(productItemResponse.ProductGroup.Id),
                Name = productItemResponse.ProductGroup.Name
            },
            Colors = string.IsNullOrEmpty(productItemResponse.Colors) ? Array.Empty<string>() : productItemResponse.Colors.Split(','),
            Images = productItemResponse.Images.Select(x => new ProductImageDto()
            {
                Id = new Guid(x.Id),
                Url = x.Url
            }),
            AverageRating = item.Rating,
            NumberOfRating = item.NumberOfRating,
            DiscountPrice = Helpers.DecimalValueHelper.ToDecimal(item.DiscountPrice),
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