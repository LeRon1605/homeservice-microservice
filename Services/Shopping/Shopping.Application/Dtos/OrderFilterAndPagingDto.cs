using BuildingBlocks.Application.Dtos;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos;

public class OrderFilterAndPagingDto : PagingParameters
{
    public OrderStatus? Status { get; set; }
}