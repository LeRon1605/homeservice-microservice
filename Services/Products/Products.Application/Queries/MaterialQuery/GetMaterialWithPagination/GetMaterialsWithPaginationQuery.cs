using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Products.Application.Dtos;

namespace Products.Application.Queries.MaterialQuery.GetMaterialWithPagination;

public class GetMaterialsWithPaginationQuery : PagingParameters ,IQuery<PagedResult<GetMaterialDto>>
{
    public string? Search { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public bool? IsObsolete { get; set; }
    public Guid? TypeId { get; set; }
    

    public GetMaterialsWithPaginationQuery(MaterialFilterAndPagingDto materialFilterAndPagingDto)
    {
        Search = materialFilterAndPagingDto.Search;
        PageIndex = materialFilterAndPagingDto.PageIndex;
        PageSize = materialFilterAndPagingDto.PageSize;
        IsObsolete = materialFilterAndPagingDto.IsObsolete;
        TypeId = materialFilterAndPagingDto.TypeId;
        
    }
}