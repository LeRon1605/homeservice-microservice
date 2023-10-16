using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Specifications;

namespace Contracts.Application.Queries.Contracts.GetContractsOfCustomer;

public class ContractsOfCustomerQueryHandler : IQueryHandler<ContractsOfCustomerQuery, IEnumerable<ContractsOfCustomerDto>>
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IMapper _mapper;

    public ContractsOfCustomerQueryHandler(IReadOnlyRepository<Contract> contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContractsOfCustomerDto>> Handle(ContractsOfCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var contractOfCustomerSpecification =
            new ContractOfCustomerSpecification(request.CustomerId, request.ContractNo);
        var contracts = await _contractRepository.FindListAsync(contractOfCustomerSpecification);

        return _mapper.Map<IEnumerable<ContractsOfCustomerDto>>(contracts);
    }
}