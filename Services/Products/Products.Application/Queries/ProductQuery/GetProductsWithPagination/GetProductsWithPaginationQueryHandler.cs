using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Queries.ProductQuery.GetProductsWithPagination;

public class GetProductsWithPaginationQueryHandler : IQueryHandler<GetProductsWithPaginationQuery, PagedResult<GetProductDto>>
{
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IReadOnlyRepository<Product> productRepository,
                                                 IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetProductDto>> Handle(GetProductsWithPaginationQuery request,
                                                   CancellationToken cancellationToken)
    {
        var getProductsSpec = new ProductsWithPaginationSpec(request.Search, request.PageIndex, request.PageSize, 
            request.IsObsolete, request.GroupId, request.TypeId);
        
        var (products, totalCount) = await _productRepository.FindWithTotalCountAsync(getProductsSpec);
        var productsDto = _mapper.Map<IEnumerable<GetProductDto>>(products);
        
        return new PagedResult<GetProductDto>(productsDto, totalCount, request.PageIndex, request.PageSize);
    }
}