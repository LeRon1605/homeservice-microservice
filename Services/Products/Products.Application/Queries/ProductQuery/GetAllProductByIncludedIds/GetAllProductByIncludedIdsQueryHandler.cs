using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Queries.ProductQuery.GetAllProductByIncludedIds;

public class GetAllProductByIncludedIdsQueryHandler : IQueryHandler<GetAllProductByIncludedIdsQuery, IEnumerable<GetProductDto>>
{
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    
    public GetAllProductByIncludedIdsQueryHandler(
        IReadOnlyRepository<Product> productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetProductDto>> Handle(GetAllProductByIncludedIdsQuery request, CancellationToken cancellationToken)
    {
        var ids = request.Ids.Distinct();
        var products = await _productRepository.FindListAsync(new ProductByIncludedIdsSpecification(ids));

        if (products.Count() != ids.Count())
        {
            throw new ProductNotFoundException(request.Ids.First(x => !products.Select(p => p.Id).Contains(x)));
        }
        
        return _mapper.Map<IEnumerable<GetProductDto>>(products);
    }
}