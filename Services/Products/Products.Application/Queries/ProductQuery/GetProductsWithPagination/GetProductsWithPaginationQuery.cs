using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Products.Application.Dtos;

namespace Products.Application.Queries.ProductQuery.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : PagingParameters, IQuery<PagedResult<GetProductDto>>
{
    public bool? IsObsolete { get; set; } 
    public string? GroupId { get; set; }
    public string? TypeId { get; set; }
}