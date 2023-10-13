namespace Products.Application.Dtos.Products;

public class ProductImageDto
{
    public Guid Id { get; set; }
    public string Url { get;  set; } = null!;
    public Guid ProductId { get;  set; }
}