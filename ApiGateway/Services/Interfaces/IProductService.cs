using ApiGateway.Dtos.Products;
using BuildingBlocks.Application.Dtos;

namespace ApiGateway.Services.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductData>> GetPagedAsync(GetProductWithFilterAndPaginationDto dto);
}