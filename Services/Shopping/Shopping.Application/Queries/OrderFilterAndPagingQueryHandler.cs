using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Shopping.Application.Dtos;
using Shopping.Domain.ShoppingAggregate;
using Shopping.Domain.ShoppingAggregate.Specifications;

namespace Shopping.Application.Queries;

public class OrderFilterAndPagingQueryHandler: IQueryHandler<OrderFilterAndPagingQuery, PagedResult<OrderFilterAndPagingDto>>
{
    private readonly IReadOnlyRepository<Order> _orderRepository;
    private readonly IMapper _mapper;

    public OrderFilterAndPagingQueryHandler(IReadOnlyRepository<Order> productRepository,
        IMapper mapper)
    {
        _orderRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<OrderFilterAndPagingDto>> Handle(OrderFilterAndPagingQuery request,
        CancellationToken cancellationToken)
    {
        var getOrderSpecification = new OrderFilterSpecification(request.Search, request.PageIndex, request.PageSize);
        
        var (orders, totalCount) = await _orderRepository.FindWithTotalCountAsync(getOrderSpecification);
        var orderFilterAndPagingDto = _mapper.Map<IEnumerable<OrderFilterAndPagingDto>>(orders);
        
        return new PagedResult<OrderFilterAndPagingDto>(orderFilterAndPagingDto, totalCount, request.PageIndex, request.PageSize);
    }
}