using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Taxes;
using Contracts.Domain.TaxAggregate;

namespace Contracts.Application.Queries.Taxes.GetAllTaxes;

public class GetAllTaxesQueryHandler : IQueryHandler<GetAllTaxesQuery, IEnumerable<TaxDto>>
{
    private readonly IReadOnlyRepository<Tax> _taxRepository;
    private readonly IMapper _mapper;
    
    public GetAllTaxesQueryHandler(IReadOnlyRepository<Tax> taxRepository, IMapper mapper)
    {
        _taxRepository = taxRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TaxDto>> Handle(GetAllTaxesQuery request, CancellationToken cancellationToken)
    {
        var taxes = await _taxRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TaxDto>>(taxes);
    }
}