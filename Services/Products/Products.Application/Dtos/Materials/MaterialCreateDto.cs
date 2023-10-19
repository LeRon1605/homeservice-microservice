using System.ComponentModel.DataAnnotations;

namespace Products.Application.Dtos.Materials;

public class MaterialCreateDto
{
    [Required] 
    public string MaterialCode { get; set; } = null!;

    [Required] 
    public string Name { get; set; } = null!;
    
    public Guid TypeId { get; set; }
    
    public Guid? SellUnitId { get; set; }
    
    public decimal SellPrice { get; set; }
    
    public decimal? Cost { get; set; }
    
    public bool IsObsolete { get; set; }
    
}