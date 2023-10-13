using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Products.Application.Dtos.Materials;

namespace Products.Application.Queries.MaterialQuery.GetMaterialByProduct;

public class GetMaterialByProductQuery : PagingParameters, IQuery<PagedResult<GetMaterialDto>>
{
    public Guid ProductId { get; set; }

    public GetMaterialByProductQuery(Guid productId, MaterialFilterAndPagingByProductDto dto)
    {
        ProductId = productId;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        Search = dto.Search;
    }
}