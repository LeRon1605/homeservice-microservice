using System.Text.Json.Serialization;
using BuildingBlocks.Application.Dtos;

namespace ApiGateway.Dtos.Products;

public class GetProductWithFilterAndPaginationDto : PagingParameters
{
    public Guid GroupId { get; set; }
    
    public decimal MinPrice { get; set; }
    
    public decimal MaxPrice { get; set; }
    
    public float Rating { get; set; }
    
    public bool IsDescending { get; set; } 
    
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductOrderBy OrderBy { get; init; } = ProductOrderBy.Id;
}