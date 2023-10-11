using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.ContractAggregate;

public class ContractAction : AuditableEntity
{
    public string Name { get; private set; }
    public DateTime Date { get; private set; }
    public string? Comment { get; private set; }
    
    public Guid ContractId { get; private set; }
    public Guid ActionByEmployeeId { get; private set; }
    
    public ContractAction(
        Guid contractId,
        string name, 
        DateTime date, 
        Guid actionByEmployeeId, 
        string? comment)
    {
        ContractId = Guard.Against.Null(contractId, nameof(ContractId));
        Name = Guard.Against.NullOrEmpty(name, nameof(Name));
        Date = Guard.Against.Null(date, nameof(Date));
        ActionByEmployeeId = Guard.Against.Null(actionByEmployeeId, nameof(ActionByEmployeeId));
        Comment = comment;
    }

    private ContractAction()
    {
        
    }
}