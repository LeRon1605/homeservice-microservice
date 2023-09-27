using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries;

public class GetContractByIdQueryHandler : IQueryHandler<GetContractByIdQuery, ContractDto>
{
    public Task<ContractDto> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ContractDto()
        {
            Id = Guid.NewGuid()
        });
    }
}