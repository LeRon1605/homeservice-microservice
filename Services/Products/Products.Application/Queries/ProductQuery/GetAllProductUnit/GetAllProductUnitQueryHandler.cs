using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Queries.ProductQuery.GetAllProductUnit;

public class GetAllProductUnitQueryHandler : IQueryHandler<GetAllProductUnitQuery, IEnumerable<ProductUnitDto>>
{
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IMapper _mapper;

    public GetAllProductUnitQueryHandler(IReadOnlyRepository<ProductUnit> productUnitRepository,
                                         IMapper mapper)
    {
        _productUnitRepository = productUnitRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductUnitDto>> Handle(GetAllProductUnitQuery request,
                                                          CancellationToken cancellationToken)
    {
        var productUnits = await _productUnitRepository.GetAllAsync();
        var productUnitsDto = _mapper.Map<IEnumerable<ProductUnitDto>>(productUnits);
        
        return productUnitsDto;
    }
}