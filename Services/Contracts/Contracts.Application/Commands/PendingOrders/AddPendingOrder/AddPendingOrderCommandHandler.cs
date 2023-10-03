using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.PendingOrdersAggregate;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.PendingOrders.AddPendingOrder;

public class AddPendingOrderCommandHandler : ICommandHandler<AddPendingOrderCommand>
{
    private readonly IRepository<PendingOrder> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPendingOrderCommandHandler> _logger;
    
    public AddPendingOrderCommandHandler(
        IRepository<PendingOrder> orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPendingOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(AddPendingOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new PendingOrder(
            request.OrderId, 
            request.BuyerId, 
            request.CustomerName, 
            request.ContactName, 
            request.Email, 
            request.Phone, 
            request.Address, 
            request.City, 
            request.State, 
            request.PostalCode);
        
        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("New pending order added: {OrderId}", order.Id);
    }
}