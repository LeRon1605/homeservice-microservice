using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Specifications;

namespace Contracts.Application.Queries.Contracts.GetContractsQuery;

public class GetContractsQueryHandler : IQueryHandler<GetContractsQuery, PagedResult<ContractDto>>
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IMapper _mapper;

    public GetContractsQueryHandler(IMapper mapper, IReadOnlyRepository<Contract> contractRepository)
    {
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public async Task<PagedResult<ContractDto>> Handle(Contracts.GetContractsQuery.GetContractsQuery request, CancellationToken cancellationToken)
    {
        var getContractsSpec = new GetContractsSpecification(search: request.Search, 
                                    pageSize: request.PageSize, pageIndex: request.PageIndex, 
                                    fromDate: request.FromDate, toDate: request.ToDate, 
                                    statusList: request.Statuses, dateType: request.FilterDateType, 
                                    salePersonId: request.SalePersonId, customerServiceRepId: request.CustomerServiceRepId);
        
        var (contracts, totalCount) = await _contractRepository.FindWithTotalCountAsync(getContractsSpec);
        
        var contractsDto = _mapper.Map<List<ContractDto>>(contracts);
        
        return new PagedResult<ContractDto>(contractsDto, totalCount, request.PageIndex, request.PageSize);
    }
}