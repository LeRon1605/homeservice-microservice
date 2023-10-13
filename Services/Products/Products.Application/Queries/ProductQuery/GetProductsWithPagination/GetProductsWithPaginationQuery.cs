using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Queries.ProductQuery.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : PagingParameters, IQuery<PagedResult<GetProductDto>>
{
    public bool? IsObsolete { get; set; } 
    public Guid? GroupId { get; set; }
    public Guid? TypeId { get; set; }
}