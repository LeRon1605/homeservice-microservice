using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Application.Dtos;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.BuyerAggregate.Exceptions;
using Shopping.Domain.OrderAggregate.Exceptions;

namespace Shopping.Application.Queries.BuyerQueries;

public class BuyerByIdQueryHandler : IQueryHandler<BuyerByIdQuery, BuyerDto>
{
    private readonly IReadOnlyRepository<Buyer> _buyerRepository;
    private readonly IMapper _mapper;

    public BuyerByIdQueryHandler(IReadOnlyRepository<Buyer> buyerRepository,
                                    IMapper mapper)
    {
        _buyerRepository = buyerRepository;
        _mapper = mapper;
    }

    public async Task<BuyerDto> Handle(BuyerByIdQuery request,
                                           CancellationToken cancellationToken)
    {
        var customer = await _buyerRepository.GetByIdAsync(request.Id) 
                       ?? throw new BuyerNotFoundException(request.Id);
        return _mapper.Map<BuyerDto>(customer);
    }
}