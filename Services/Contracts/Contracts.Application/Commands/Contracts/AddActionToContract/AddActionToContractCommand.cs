using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;

namespace Contracts.Application.Commands.Contracts.AddActionToContract;

public class AddActionToContractCommand : ContractActionCreateDto, ICommand<ContractActionDto>
{
    public Guid ContractId { get; set; }
    
    public AddActionToContractCommand(Guid contractId, ContractActionCreateDto dto)
    {
        ContractId = contractId;
        Name = dto.Name;
        Date = dto.Date;
        Comment = dto.Comment;
        ActionByEmployeeId = dto.ActionByEmployeeId;
    }
}