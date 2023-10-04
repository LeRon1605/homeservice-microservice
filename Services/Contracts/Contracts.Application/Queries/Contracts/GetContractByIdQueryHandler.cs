using BuildingBlocks.Application.Cache;
using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries.Contracts;

public class GetContractByIdQueryHandler : IQueryHandler<GetContractByIdQuery, ContractDto>
{
    private readonly ICacheService _cache;

    public GetContractByIdQueryHandler(ICacheService cache)
    {
        _cache = cache;
    }
    
    public async Task<ContractDto> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _cache.GetCachedDataAsync<ContractDto>("test");
        
        if (data == null)
        {
            data = new ContractDto()
            {
                Id = Guid.NewGuid()
            };
            await _cache.SetCachedDataAsync("test", data, TimeSpan.FromMinutes(1));
        }

        return data;
    }
}