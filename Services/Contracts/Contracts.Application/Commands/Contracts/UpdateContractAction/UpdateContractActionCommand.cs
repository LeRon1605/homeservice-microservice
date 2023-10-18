using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractUpdate;

namespace Contracts.Application.Commands.Contracts.UpdateContractAction;

public class UpdateContractActionCommand : ContractActionUpdateDto, ICommand<ContractActionDto>
{
    public Guid ContractId { get; set; }
    public Guid ActionId { get; set; }
    
    public UpdateContractActionCommand(Guid contractId, Guid actionId, ContractActionUpdateDto dto)
    {
        ContractId = contractId;
        ActionId = actionId;
        Name = dto.Name;
        Date = dto.Date;
        Comment = dto.Comment;
        ActionByEmployeeId = dto.ActionByEmployeeId;
    }
}