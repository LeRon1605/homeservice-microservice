using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductTypeAggregate;

namespace Products.Application.Queries.ProductQuery.GetAllProductType;

public class GetAllProductTypeQueryHandler : IQueryHandler<GetAllProductTypeQuery, IEnumerable<ProductTypeDto>>
{
    private readonly IReadOnlyRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;

    public GetAllProductTypeQueryHandler(IReadOnlyRepository<ProductType> productTypeRepository,
                                         IMapper mapper)
    {
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductTypeDto>> Handle(GetAllProductTypeQuery request,
                                                    CancellationToken cancellationToken)
    {
        var productTypes = await _productTypeRepository.GetAllAsync();
        var productTypesDto = _mapper.Map<IEnumerable<ProductTypeDto>>(productTypes);
        
        return productTypesDto;
    }
}