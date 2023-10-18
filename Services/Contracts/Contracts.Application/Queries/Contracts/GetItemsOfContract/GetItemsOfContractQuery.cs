using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries.Contracts.GetItemsOfContract;

public class GetItemsOfContractQuery : IQuery<IEnumerable<ContractLineDto>>
{
    public Guid ContractId { get; set; }
    
    public GetItemsOfContractQuery(Guid contractId)
    {
        ContractId = contractId;
    }
}