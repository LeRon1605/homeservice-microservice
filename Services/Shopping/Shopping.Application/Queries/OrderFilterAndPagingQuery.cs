using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Shopping.Application.Dtos;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Queries;

public class OrderFilterAndPagingQuery : PagingParameters, IQuery<PagedResult<OrderDto>>
{
    public OrderStatus? Status { get; set; }
    
    public OrderFilterAndPagingQuery(OrderFilterAndPagingDto orderFilterAndPagingDto)
    {
        Search = orderFilterAndPagingDto.Search;
        PageIndex = orderFilterAndPagingDto.PageIndex;
        PageSize = orderFilterAndPagingDto.PageSize;
        Status = orderFilterAndPagingDto.Status;
    }
}