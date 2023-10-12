using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;

namespace Contracts.Application.Queries.Contracts.GetPaymentsOfContract;

public class GetPaymentsOfContractQueryHandler : IQueryHandler<GetPaymentsOfContractQuery, PagedResult<ContractPaymentDto>>
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<ContractPayment> _contractPaymentRepository;
    private readonly IMapper _mapper;
    
    public GetPaymentsOfContractQueryHandler(
        IReadOnlyRepository<Contract> contractRepository,
        IReadOnlyRepository<ContractPayment> contractPaymentRepository,
        IMapper mapper)
    {
        _contractRepository = contractRepository;
        _contractPaymentRepository = contractPaymentRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<ContractPaymentDto>> Handle(GetPaymentsOfContractQuery request, CancellationToken cancellationToken)
    {
        var isContractExist = await _contractRepository.AnyAsync(request.ContractId);
        if (!isContractExist)
        {
            throw new ContractNotFoundException(request.ContractId);
        }

        var specification = new PaymentOfContractSpecification(request.Search, request.PageSize, request.PageIndex, request.ContractId, request.IsShowDeleted);
        var (payments, totalCount) = await _contractPaymentRepository.FindWithTotalCountAsync(specification);
        
        return new PagedResult<ContractPaymentDto>(
            _mapper.Map<IEnumerable<ContractPaymentDto>>(payments), 
            totalCount, 
            request.PageIndex, 
            request.PageSize);
    }
}