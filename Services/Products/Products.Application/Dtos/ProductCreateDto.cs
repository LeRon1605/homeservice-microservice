using System.ComponentModel.DataAnnotations;

namespace Products.Application.Dtos;

public class ProductCreateDto
{
    [Required] public string ProductCode { get; set; }
    [Required] public string Name { get; set; }
    [Required] public Guid TypeId { get; set; }
    [Required] public Guid GroupId { get; set; }
    public Guid BuyUnitId { get; set; }
    public Guid SellUnitId { get; set; }
    public decimal? Buy { get; set; }
    public decimal? Sell { get; set; }
    public bool IsObsolete { get; set; }
    public string? Description { get; set; }
    
    public string[] Urls { get;  set; }
}