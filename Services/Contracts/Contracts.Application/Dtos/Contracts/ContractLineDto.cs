namespace Contracts.Application.Dtos.Contracts;

public class ContractLineDto
{
    public Guid ContractId { get; set; }
    
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    
    public Guid UnitId { get; set; }
    public string UnitName { get; set; } = null!;
    
    public Guid? TaxId { get; set; }
    public string? TaxName { get; set; }
    
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal SellPrice { get; set; }
}