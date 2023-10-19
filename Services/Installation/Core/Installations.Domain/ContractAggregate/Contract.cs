using BuildingBlocks.Domain.Models;

namespace Installations.Domain.ContractAggregate;

public class Contract : AggregateRoot
{
    public int ContractNo { get; private set; }
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; } = null!;
    public InstallationAddress? InstallationAddress { get; private set; }
    public ICollection<ContractLine> ContractLines { get; private set; } = new List<ContractLine>();
    
    private Contract()
    {
    }
    
    public Contract(
        Guid id,
        int contractNo,
        Guid customerId,
        string customerName,
        InstallationAddress? installationAddress = null)
    {
        Id = id;
        ContractNo = contractNo;
        CustomerId = customerId;
        CustomerName = customerName;
        InstallationAddress = installationAddress;
    }
    
    public void AddContractLine(Guid contractLineId, Guid productId, string productName, string? color)
    {
        var contractLine = new ContractLine(contractLineId, Id, productId, productName, color);
        ContractLines.Add(contractLine);
    }
    
}