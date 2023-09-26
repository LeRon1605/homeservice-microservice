using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Application.Dtos;
using Shopping.Domain.Exceptions;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Commands;

public class OrderRejectCommandHandler : ICommandHandler<OrderRejectCommand, OrderDto>
{
    private readonly IRepository<Order> _oderRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderRejectCommandHandler(IRepository<Order> oderRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _oderRepository = oderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<OrderDto> Handle(OrderRejectCommand request, CancellationToken cancellationToken)
    {
        var order = await _oderRepository.GetByIdAsync(request.Id);
        if (order == null)
            throw new OrderNotFoundException(request.Id);
        order.Reject();
        _oderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderDto>(order);
    }
}