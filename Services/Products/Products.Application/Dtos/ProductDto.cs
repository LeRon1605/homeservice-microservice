namespace Products.Application.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsObsolete { get; set; }
    
    public ProductPriceDto? Sell { get; set; }
    public ProductPriceDto? Buy { get; set; }
    
    public ProductTypeDto Type { get; set; }
    public ProductGroupDto Group { get; set; }
}

public class ProductPriceDto
{
    public decimal? Price { get; set; }
    public ProductUnitDto? Unit { get; set; }
}