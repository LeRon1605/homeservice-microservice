namespace Products.Application.Dtos;

public class GetMaterialDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public ProductTypeDto ProductType { get; set; } = null!;
    
    public ProductUnitDto? SellUnit { get; set; }
    
    public decimal? SellPrice { get; set; }
    
    public decimal? Cost { get; set; }
    
    public bool IsObsolete { get; set; }
}