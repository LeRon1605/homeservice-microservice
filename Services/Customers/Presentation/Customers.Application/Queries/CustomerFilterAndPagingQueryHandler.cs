using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Customers.Application.Dtos;
using Customers.Domain.CustomerAggregate;
using Customers.Domain.CustomerAggregate.Specifications;

namespace Customers.Application.Queries;

public class CustomerFilterAndPagingQueryHandler: IQueryHandler<CustomerFilterAndPagingQuery, PagedResult<CustomerFilterAndPagingDto>>
{
    private readonly IReadOnlyRepository<Customer> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IReadOnlyRepository<Customer> productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<CustomerFilterAndPagingDto>> Handle(CustomerFilterAndPagingQuery request,
        CancellationToken cancellationToken)
    {
        var getProductsSpec = new CustomerFilterSpecification(request.Search, request.PageIndex, request.PageSize);
        
        var (products, totalCount) = await _productRepository.FindWithTotalCountAsync(getProductsSpec);
        var productsDto = _mapper.Map<IEnumerable<CustomerFilterAndPagingDto>>(products);
        
        return new PagedResult<CustomerFilterAndPagingDto>(productsDto, totalCount, request.PageIndex, request.PageSize);
    }
}