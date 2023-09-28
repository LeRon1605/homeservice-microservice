using ApiGateway.Dtos.Products;
using BuildingBlocks.Application.Dtos;

namespace ApiGateway.Services.Interfaces;

public interface IProductService
{
    PagedResult<ProductData> GetPaged(GetProductWithFilterAndPaginationDto dto);
    
    Task<ProductData> GetByIdAsync(Guid id);
    
    Task<IEnumerable<ProductData>> GetProductByIncludedIdsAsync(IEnumerable<Guid> ids);
}