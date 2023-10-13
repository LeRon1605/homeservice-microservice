using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos.Materials;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.Specifications;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Exceptions;

namespace Products.Application.Queries.MaterialQuery.GetMaterialByProduct;

public class GetMaterialByProductQueryHandler : IQueryHandler<GetMaterialByProductQuery, PagedResult<GetMaterialDto>>
{
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    
    public GetMaterialByProductQueryHandler(
        IReadOnlyRepository<Material> materialRepository,
        IReadOnlyRepository<Product> productRepository,
        IMapper mapper)
    {
        _materialRepository = materialRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<GetMaterialDto>> Handle(GetMaterialByProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }
        
        var specification = new MaterialByProductTypeSpecification(product.ProductTypeId, request.Search, request.PageIndex, request.PageSize);
        var (materials, totalCount) = await _materialRepository.FindWithTotalCountAsync(specification);

        return new PagedResult<GetMaterialDto>(
            _mapper.Map<IEnumerable<GetMaterialDto>>(materials), 
            totalCount, 
            request.PageIndex, 
            request.PageSize);
    }
}