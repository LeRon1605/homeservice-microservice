using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Shopping.Application.Dtos;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.OrderAggregate.Specifications;

namespace Shopping.Application.Queries;

public class OrderFilterAndPagingQueryHandler : IQueryHandler<OrderFilterAndPagingQuery, PagedResult<OrderDto>>
{
    private readonly IReadOnlyRepository<Order> _oderRepository;
    private readonly IMapper _mapper;

    public OrderFilterAndPagingQueryHandler(IReadOnlyRepository<Order> oderRepository, IMapper mapper)
    {
        _oderRepository = oderRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<OrderDto>> Handle(OrderFilterAndPagingQuery request, CancellationToken cancellationToken)
    {
        // var orderSpecification = new OrderFilterAndPagingSpecification(request.Search, request.PageIndex, request.PageSize, request.Status);
        //
        // var (orders, totalCount) = await _oderRepository.FindWithTotalCountAsync(orderSpecification);
        // var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
        //
        // return new PagedResult<OrderDto>(ordersDto, totalCount, request.PageIndex, request.PageSize);
        return null;
    }
}