using System.ComponentModel.DataAnnotations;

namespace Products.Application.Dtos;

public class ProductUpdateDto
{
    [Required] 
    public string ProductCode { get; set; } = null!;
    
    [Required] 
    public string Name { get; set; } = null!;
    
    [Required] 
    public Guid TypeId { get; set; }
    
    [Required] 
    public Guid GroupId { get; set; }
    
    public Guid BuyUnitId { get; set; }
    public Guid SellUnitId { get; set; }
    public decimal? BuyPrice { get; set; }
    
    [Required]
    public decimal? SellPrice { get; set; }
    
    public bool IsObsolete { get; set; }
    public string? Description { get; set; }
    public string[] Urls { get;  set; } = null!;
    public IEnumerable<string>? Colors { get; set; }
}