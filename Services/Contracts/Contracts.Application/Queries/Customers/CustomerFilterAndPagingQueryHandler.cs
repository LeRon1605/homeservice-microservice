using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Customers;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.CustomerAggregate.Specifications;

namespace Contracts.Application.Queries.Customers;

public class CustomerFilterAndPagingQueryHandler : IQueryHandler<CustomerFilterAndPagingQuery, PagedResult<CustomerDto>>
{
    private readonly IReadOnlyRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomerFilterAndPagingQueryHandler(IReadOnlyRepository<Customer> productRepository,
        IMapper mapper)
    {
        _customerRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<CustomerDto>> Handle(CustomerFilterAndPagingQuery request,
        CancellationToken cancellationToken)
    {
        var getCustomerSpecification = new CustomerFilterSpecification(request.Search, request.PageIndex, request.PageSize);
        
        var (customers, totalCount) = await _customerRepository.FindWithTotalCountAsync(getCustomerSpecification);
        var customerFilterAndPagingDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        
        return new PagedResult<CustomerDto>(customerFilterAndPagingDto, totalCount, request.PageIndex, request.PageSize);
    }
}