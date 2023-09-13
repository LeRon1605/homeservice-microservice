using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Shopping.Application.Dtos;

namespace Shopping.Application.Queries;

public class OrderFilterAndPagingQuery  : PagingParameters, IQuery<PagedResult<OrderFilterAndPagingDto>>
{
    
}