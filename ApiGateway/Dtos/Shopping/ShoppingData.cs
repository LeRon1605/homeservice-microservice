namespace ApiGateway.Dtos.Shopping;

public class ShoppingData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; 
    public double AverageRating { get; set; }
    public decimal OriginPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int NumberOfRating { get; set; }
    public int NumberOfOrder { get; set; }
}