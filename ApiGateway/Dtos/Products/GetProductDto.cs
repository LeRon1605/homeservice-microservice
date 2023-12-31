namespace ApiGateway.Dtos.Products;

public class GetProductDto
{
    public Guid Id { get; set; }
    public string ProductCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsObsolete { get; set; }
    
    public decimal? BuyPrice { get; set; }
    public decimal? SellPrice { get; set; }

    public IEnumerable<string> Colors { get; set; } = null!;
    
    public ProductUnitDto? BuyUnit { get; set; }
    
    public ProductUnitDto? SellUnit { get; set; }
    public ProductTypeDto Type { get; set; } = null!;
    public ProductGroupDto Group { get; set; } = null!;
    public ProductImageDto[] Images { get; set; } = null!;
} 
