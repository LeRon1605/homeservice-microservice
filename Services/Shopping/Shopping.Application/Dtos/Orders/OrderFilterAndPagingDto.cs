using System.Text.Json.Serialization;
using BuildingBlocks.Application.Dtos;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos.Orders;

public class OrderFilterAndPagingDto : PagingParameters
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public List<OrderStatus>? Status { get; set; }
}