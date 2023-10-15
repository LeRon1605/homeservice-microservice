using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Taxes;
using Contracts.Domain.TaxAggregate;
using Contracts.Domain.TaxAggregate.Exceptions;

namespace Contracts.Application.Queries.Taxes.GetTaxById;

public class GetTaxByIdQueryHandler : IQueryHandler<GetTaxByIdQuery, TaxDto>
{
    private readonly IReadOnlyRepository<Tax> _taxRepository;
    private readonly IMapper _mapper;
    
    public GetTaxByIdQueryHandler(IReadOnlyRepository<Tax> taxRepository, IMapper mapper)
    {
        _taxRepository = taxRepository;
        _mapper = mapper;
    }
    
    public async Task<TaxDto> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
    {
        var tax = await _taxRepository.GetByIdAsync(request.TaxId);
        if (tax == null)
        {
            throw new TaxNotFoundException(request.TaxId);
        }

        return _mapper.Map<TaxDto>(tax);
    }
}