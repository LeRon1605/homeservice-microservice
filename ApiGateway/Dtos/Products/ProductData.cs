namespace ApiGateway.Dtos.Products;

public class ProductData
{
    public Guid Id { get; set; }
    public string ProductCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsObsolete { get; set; }
    
    public double AverageRating { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int NumberOfRating { get; set; }
    public int NumberOfOrder { get; set; }
    
    public decimal? BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
    
    public ProductUnitDto? BuyUnit { get; set; }
    
    public ProductUnitDto? SellUnit { get; set; }
    public ProductTypeDto Type { get; set; }
    public ProductGroupDto Group { get; set; }
    public IEnumerable<ProductImageDto> Images { get; set; }
}