using System.ComponentModel.DataAnnotations;

namespace Products.Application.Dtos;

public class MaterialUpdateDto
{
    [Required] 
    public string MaterialCode { get; set; } = null!;

    [Required] 
    public string Name { get; set; } = null!;
    
    public Guid ProductTypeId { get; set; }
    
    public Guid? SellUnitId { get; set; }
    
    public decimal? SellPrice { get; set; }
    
    public decimal? Cost { get; set; }
    
    public bool IsObsolete { get; set; }
}