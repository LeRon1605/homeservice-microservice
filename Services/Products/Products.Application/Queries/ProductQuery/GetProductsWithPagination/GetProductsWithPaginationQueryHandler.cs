using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Queries.ProductQuery.GetProductsWithPagination;

public class GetProductsWithPaginationQueryHandler : IQueryHandler<GetProductsWithPaginationQuery, PagedResult<ProductDto>>
{
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IReadOnlyRepository<Product> productRepository,
                                                 IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetProductsWithPaginationQuery request,
                                                   CancellationToken cancellationToken)
    {
        var getProductsSpec = new ProductsWithPaginationSpec(request.Search, request.PageIndex, request.PageSize);
        
        var (products, totalCount) = await _productRepository.FindWithTotalCountAsync(getProductsSpec);
        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        return new PagedResult<ProductDto>(productsDto, totalCount, request.PageIndex, request.PageSize);
    }
}