using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.PendingOrdersAggregate;
using Contracts.Domain.ProductAggregate.Exceptions;

namespace Contracts.Application.Commands.PendingOrders.DeletePendingOrder;

public class DeletePendingOrderCommandHandler : ICommandHandler<DeletePendingOrderCommand>
{
    private readonly IRepository<PendingOrder> _pendingOrderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePendingOrderCommandHandler(IRepository<PendingOrder> pendingOrderRepository, IUnitOfWork unitOfWork)
    {
        _pendingOrderRepository = pendingOrderRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeletePendingOrderCommand request, CancellationToken cancellationToken)
    {
        var pendingOrder = await _pendingOrderRepository.GetByIdAsync(request.Id);
        if (pendingOrder == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        _pendingOrderRepository.Delete(pendingOrder);
        await _unitOfWork.SaveChangesAsync();
    }
}