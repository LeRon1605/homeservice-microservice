using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;

namespace Contracts.Application.Queries.Contracts.GetItemsOfContract;

public class GetItemsOfContractQueryHandler : IQueryHandler<GetItemsOfContractQuery, IEnumerable<ContractLineDto>>
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<ContractLine> _contractLineRepository;
    private readonly IMapper _mapper;
    
    public GetItemsOfContractQueryHandler(
        IReadOnlyRepository<Contract> contractRepository, 
        IReadOnlyRepository<ContractLine> contractLineRepository, 
        IMapper mapper)
    {
        _contractRepository = contractRepository;
        _contractLineRepository = contractLineRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ContractLineDto>> Handle(GetItemsOfContractQuery request, CancellationToken cancellationToken)
    {
        if (!await _contractRepository.AnyAsync(request.ContractId))
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        var contractLines = await _contractLineRepository.FindListAsync(new ContractLineByContractSpecification(request.ContractId));
        return _mapper.Map<IEnumerable<ContractLineDto>>(contractLines);
    }
}