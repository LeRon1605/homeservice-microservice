using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos.Products;
using Products.Domain.ProductUnitAggregate;
using Products.Domain.ProductUnitAggregate.Exceptions;

namespace Products.Application.Queries.ProductQuery.GetProductUnitById;

public class GetProductUnitByIdQueryHandler : IQueryHandler<GetProductUnitByIdQuery, ProductUnitDto>
{
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IMapper _mapper;
    
    public GetProductUnitByIdQueryHandler(IReadOnlyRepository<ProductUnit> productUnitRepository, IMapper mapper)
    {
        _productUnitRepository = productUnitRepository;
        _mapper = mapper;
    }
    
    public async Task<ProductUnitDto> Handle(GetProductUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var productUnit = await _productUnitRepository.GetByIdAsync(request.ProductUnitId);
        if (productUnit == null)
        {
            throw new ProductUnitNotFoundException(request.ProductUnitId);
        }

        return _mapper.Map<ProductUnitDto>(productUnit);
    }
}