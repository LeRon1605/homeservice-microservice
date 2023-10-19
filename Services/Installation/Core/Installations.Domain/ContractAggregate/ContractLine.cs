using BuildingBlocks.Domain.Models;

namespace Installations.Domain.ContractAggregate;

public class ContractLine : Entity
{
    public Guid ContractId { get; private set; }
    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; }
    public string? Color { get; private set; }
    
    private ContractLine(string productName)
    {
        ProductName = productName;
    }
    
    public ContractLine(
        Guid id,
        Guid contractId,
        Guid productId,
        string productName,
        string? color = null)
    {
        Id = id;
        ContractId = contractId;
        ProductId = productId;
        ProductName = productName;
        Color = color;
    }
}