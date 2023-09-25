using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlocks.Application.Dtos;

namespace ApiGateway.Dtos.Products;

public class GetProductWithFilterAndPaginationDto : PagingParameters
{
    public Guid? GroupId { get; set; }
    
    public decimal? MinPrice { get; set; }
    
    public decimal? MaxPrice { get; set; }
    
    public double? Rating { get; set; }
    
    public bool IsDescending { get; set; } 
    
    [EnumDataType(typeof(ProductOrderBy))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductOrderBy? OrderBy { get; init; }
}