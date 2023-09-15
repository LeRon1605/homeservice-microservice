using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductGroupAggregate;

namespace Products.Application.Queries.ProductQuery.GetAllProductGroup;

public class GetAllProductGroupQueryHandler : IQueryHandler<GetAllProductGroupQuery, IEnumerable<ProductGroupDto>>
{
    private readonly IReadOnlyRepository<ProductGroup> _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductGroupQueryHandler(IReadOnlyRepository<ProductGroup> productRepository,
                                          IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductGroupDto>> Handle(GetAllProductGroupQuery request,
                                                     CancellationToken cancellationToken)
    {
        var productGroups = await _productRepository.GetAllAsync();
        var productGroupsDto = _mapper.Map<IEnumerable<ProductGroupDto>>(productGroups);
        
        return productGroupsDto; 
    }
}