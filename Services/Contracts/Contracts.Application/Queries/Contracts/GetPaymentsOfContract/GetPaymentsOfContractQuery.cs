using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries.Contracts.GetPaymentsOfContract;

public class GetPaymentsOfContractQuery : PaymentsOfContractFilterDto, IQuery<PagedResult<ContractPaymentDto>>
{
    public Guid ContractId { get; set; }
    
    public GetPaymentsOfContractQuery(Guid contractId, PaymentsOfContractFilterDto dto)
    {
        ContractId = contractId;
        IsShowDeleted = dto.IsShowDeleted;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        Search = dto.Search;
    }
}