using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Contracts.DeleteActionFromContract;

public class DeleteActionFromContractCommand : ICommand
{
    public Guid ContractId { get; set; }
    public Guid ActionId { get; set; }
    
    public DeleteActionFromContractCommand(Guid contractId, Guid actionId)
    {
        ContractId = contractId;
        ActionId = actionId;
    }
}