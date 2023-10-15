namespace Contracts.Application.Dtos.Contracts.ContractUpdate;

public class ContractLineUpdateDto
{
    public Guid? Id { get; set; }
    
    public Guid ProductId { get; set; }
    public Guid UnitId { get; set; }
    public Guid? TaxId { get; set; }
    
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal SellPrice { get; set; }
}