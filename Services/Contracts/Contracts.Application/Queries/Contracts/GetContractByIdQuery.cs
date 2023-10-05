using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries.Contracts;

public class GetContractByIdQuery : IQuery<ContractDetailDto>
{
    public Guid Id { get; set; }
    
    public GetContractByIdQuery(Guid id)
    {
        Id = id;
    }
}