using AutoMapper;
using BuildingBlocks.Application.Cache;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Specifications;

namespace Contracts.Application.Queries.Contracts;

public class GetContractByIdQueryHandler : IQueryHandler<GetContractByIdQuery, ContractDetailDto>
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IMapper _mapper;

    public GetContractByIdQueryHandler(IReadOnlyRepository<Contract> contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<ContractDetailDto> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
    {
        var contractDetailSpecification = new ContractDetailSpecification(request.Id);
        var contractDetail = await _contractRepository.FindAsync(contractDetailSpecification);

        return _mapper.Map<ContractDetailDto>(contractDetail);
    }
}