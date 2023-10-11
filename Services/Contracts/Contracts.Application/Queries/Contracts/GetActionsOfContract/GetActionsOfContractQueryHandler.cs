using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;

namespace Contracts.Application.Queries.Contracts.GetActionsOfContract;

public class GetActionsOfContractQueryHandler : IQueryHandler<GetActionsOfContractQuery, PagedResult<ContractActionDto>>
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<ContractAction> _contractActionRepository;
    private readonly IMapper _mapper;
    
    public GetActionsOfContractQueryHandler(
        IReadOnlyRepository<Contract> contractRepository,
        IReadOnlyRepository<ContractAction> contractActionRepository,
        IMapper mapper)
    {
        _contractRepository = contractRepository;
        _contractActionRepository = contractActionRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<ContractActionDto>> Handle(GetActionsOfContractQuery request, CancellationToken cancellationToken)
    {
        var isContractExist = await _contractRepository.AnyAsync(request.ContractId);
        if (!isContractExist)
        {
            throw new ContractNotFoundException(request.ContractId);
        }

        var specification = new ActionOfContractSpecification(request.Search, request.PageSize, request.PageIndex, request.ContractId);
        var (actions, totalCount) = await _contractActionRepository.FindWithTotalCountAsync(specification);
        
        return new PagedResult<ContractActionDto>(
            _mapper.Map<IEnumerable<ContractActionDto>>(actions), 
            totalCount, 
            request.PageIndex, 
            request.PageSize);
    }
}