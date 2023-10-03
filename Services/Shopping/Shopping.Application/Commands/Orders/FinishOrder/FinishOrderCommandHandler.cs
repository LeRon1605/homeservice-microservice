using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Shopping.Application.IntegrationEvents.Events.Orders;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.OrderAggregate.Exceptions;

namespace Shopping.Application.Commands.Orders.FinishOrder;

public class FinishOrderCommandHandler : ICommandHandler<FinishOrderCommand>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FinishOrderCommandHandler> _logger;
    public FinishOrderCommandHandler(
        IRepository<Order> orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<FinishOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(FinishOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order == null)
        {
            throw new OrderNotFoundException(request.Id);
        }
        
        order.Finish();
        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Order {OrderId} is successfully finished.", order.Id);
    }
}