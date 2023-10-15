namespace Contracts.Application.Dtos.Taxes;

public class TaxDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public double Value { get; set; }
}