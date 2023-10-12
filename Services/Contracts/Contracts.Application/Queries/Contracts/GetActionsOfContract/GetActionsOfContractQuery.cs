using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries.Contracts.GetActionsOfContract;

public class GetActionsOfContractQuery : ActionsOfContractFilterDto, IQuery<PagedResult<ContractActionDto>>
{
    public Guid ContractId { get; set; }
    
    public GetActionsOfContractQuery(Guid contractId, ActionsOfContractFilterDto dto)
    {
        ContractId = contractId;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        Search = dto.Search;
    }
}