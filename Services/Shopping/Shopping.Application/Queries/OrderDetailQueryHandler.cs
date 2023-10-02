using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Application.Dtos.Orders;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.OrderAggregate.Specifications;

namespace Shopping.Application.Queries;

public class OrderDetailQueryHandler : IQueryHandler<OrderDetailQuery, OrderDetailsDto>
{
    private readonly IReadOnlyRepository<Order> _oderRepository;
    private readonly IMapper _mapper;

    public OrderDetailQueryHandler(IReadOnlyRepository<Order> oderRepository, IMapper mapper)
    {
        _oderRepository = oderRepository;
        _mapper = mapper;
    }
    
    public async Task<OrderDetailsDto> Handle(OrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderDetailSpecification = new OrderDetailSpecification(request.OrderId);
        var orderDetails = await _oderRepository.FindAsync(orderDetailSpecification);
        var orderDetailsDto = _mapper.Map<OrderDetailsDto>(orderDetails);
        return orderDetailsDto;
    }
}