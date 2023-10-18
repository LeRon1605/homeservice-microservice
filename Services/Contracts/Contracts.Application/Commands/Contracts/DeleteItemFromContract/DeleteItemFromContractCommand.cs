using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Contracts.DeleteItemFromContract;

public class DeleteItemFromContractCommand : ICommand
{
    public Guid ContractId { get; set; }
    public Guid ContractLineId { get; set; }
    
    public DeleteItemFromContractCommand(Guid contractId, Guid contractLineId)
    {
        ContractId = contractId;
        ContractLineId = contractLineId;
    }
}