using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;
using Contracts.Domain.PendingOrdersAggregate;
using Contracts.Domain.PendingOrdersAggregate.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.ConvertContractFromOrder;

public class ConvertContractFromOrderCommandHandler : ICommandHandler<ConvertContractFromOrderCommand, ContractDetailDto>
{
    private readonly IRepository<PendingOrder> _pendingOrderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConvertContractFromOrderCommandHandler> _logger;
    private readonly IMediator _mediator;

    public ConvertContractFromOrderCommandHandler(
        IRepository<PendingOrder> pendingOrderRepository,
        IUnitOfWork unitOfWork,
        ILogger<ConvertContractFromOrderCommandHandler> logger,
        IMediator mediator)
    {
        _pendingOrderRepository = pendingOrderRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task<ContractDetailDto> Handle(ConvertContractFromOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _pendingOrderRepository.GetByIdAsync(request.OrderId);
        if (order == null)
        {
            throw new PendingOrderNotFound(request.OrderId);
        }

        await _unitOfWork.BeginTransactionAsync();

        await FinishOrderAsync(order);
        
        var contract = await CreateContractAsync(order, request);
        
        await _unitOfWork.CommitTransactionAsync();

        return contract;
    }

    private async Task FinishOrderAsync(PendingOrder order)
    {
        order.Finish();
        _pendingOrderRepository.Delete(order);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Finished order {OrderId} and converted to contract", order.Id);
    }

    private async Task<ContractDetailDto> CreateContractAsync(PendingOrder order, ContractConvertedFromOrderDto request)
    {
        var contractCreateDto = new ContractCreateDto()
        {
            CustomerId = order.BuyerId,
            SupervisorId = request.SupervisorId,
            InvoiceDate = request.InvoiceDate,
            SalePersonId = request.SalePersonId,
            Status = request.Status,
            CustomerNote = request.CustomerNote,
            ActualInstallationDate = request.ActualInstallationDate,
            EstimatedInstallationDate = request.EstimatedInstallationDate,
            InstallationAddress = request.InstallationAddress,
            PurchaseOrderNo = request.PurchaseOrderNo,
            CustomerServiceRepId = request.CustomerServiceRepId,
            InvoiceNo = request.InvoiceNo,
            Items = request.Items,
            Payments = request.Payments,
            Actions = request.Actions,
            SoldAt = request.SoldAt
        };
        
        return await _mediator.Send(new AddContractCommand(contractCreateDto));
    }
}