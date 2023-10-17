using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.DeletePaymentFromContract;

public class DeletePaymentFromContractCommandHandler : ICommandHandler<DeletePaymentFromContractCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePaymentFromContractCommandHandler> _logger;
    
    public DeletePaymentFromContractCommandHandler(
        IRepository<Contract> contractRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeletePaymentFromContractCommandHandler> logger)
    {
        _logger = logger;
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeletePaymentFromContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        contract.RemovePayment(request.PaymentId);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Added new payment to contract: {ContractId}", request.ContractId);
    }
}