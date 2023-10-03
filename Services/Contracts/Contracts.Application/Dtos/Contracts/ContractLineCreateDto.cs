namespace Contracts.Application.Dtos.Contracts;

public class ContractLineCreateDto
{
    public Guid ProductId { get; set; }
    public Guid UnitId { get; set; }
    
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal SellPrice { get; set; }
}