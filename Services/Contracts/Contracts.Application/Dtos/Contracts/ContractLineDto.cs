namespace Contracts.Application.Dtos.Contracts;

public class ContractLineDto
{
    public Guid Id { get; set; }
    
    public ProductInContractLineDto Product { get; set; } = null!;
    public ProductUnitInContractLineDto ProductUnit { get; set; } = null!;
    public TaxInContractLineDto Tax { get; set; } = null!;
    
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal SellPrice { get; set; }
}

public class ProductInContractLineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ProductUnitInContractLineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class TaxInContractLineDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public double? Value { get; set; }
}