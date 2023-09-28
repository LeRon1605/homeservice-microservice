using System.Text.Json.Serialization;
using BuildingBlocks.Application.Dtos;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Dtos;

public class OrderFilterAndPagingDto : PagingParameters
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus? Status { get; set; }
}