using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.PendingOrdersAggregate;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate.Exceptions;

namespace Contracts.Application.Commands;

public class RejectOrderCommandHandler : ICommandHandler<RejectOrderCommand>
{
    private readonly IRepository<PendingOrder> _pendingOrderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RejectOrderCommandHandler(IRepository<PendingOrder> pendingOrderRepository, IUnitOfWork unitOfWork)
    {
        _pendingOrderRepository = pendingOrderRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(RejectOrderCommand request, CancellationToken cancellationToken)
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