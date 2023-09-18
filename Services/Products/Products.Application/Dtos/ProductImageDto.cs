namespace Products.Application.Dtos;

public class ProductImageDto
{
    public Guid Id { get; set; }
    public string Url { get;  set; }
    public Guid ProductId { get;  set; }
}