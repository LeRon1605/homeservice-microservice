using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.PendingOrdersAggregate;
using Contracts.Domain.PendingOrdersAggregate.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.ConvertContractFromOrder;

public class ConvertContractFromOrderCommandHandler : ICommandHandler<ConvertContractFromOrderCommand, ContractDetailDto>
{
    private readonly IRepository<PendingOrder> _pendingOrderRepository;
    private readonly ILogger<ConvertContractFromOrderCommandHandler> _logger;
    private readonly IMediator _mediator;

    public ConvertContractFromOrderCommandHandler(
        IRepository<PendingOrder> pendingOrderRepository,
        ILogger<ConvertContractFromOrderCommandHandler> logger,
        IMediator mediator)
    {
        _pendingOrderRepository = pendingOrderRepository;
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

        order.Finish();
        _pendingOrderRepository.Delete(order);
        
        _logger.LogInformation("Finished order {OrderId} and converted to contract", request.OrderId);
        
        var contract = await _mediator.Send(new AddContractCommand(request), cancellationToken);
        return contract;
    }
}