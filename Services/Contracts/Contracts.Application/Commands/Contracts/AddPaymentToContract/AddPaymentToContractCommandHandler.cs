using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.PaymentMethodAggregate;
using Contracts.Domain.PaymentMethodAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.AddPaymentToContract;

public class AddPaymentToContractCommandHandler : ICommandHandler<AddPaymentToContractCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPaymentToContractCommandHandler> _logger;
    
    public AddPaymentToContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<PaymentMethod> paymentMethodRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPaymentToContractCommandHandler> logger)
    {
        _contractRepository = contractRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(AddPaymentToContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        contract.AddPayment(
            request.DatePaid,
            request.PaidAmount,
            request.Surcharge,
            request.Reference,
            request.Comments,
            request.PaymentMethodId,
            await GetPaymentMethodNameByIdAsync(request.PaymentMethodId));
        
        _contractRepository.Update(contract);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Added new payment to contract: {ContractId}", request.ContractId);
    }

    private async Task<string?> GetPaymentMethodNameByIdAsync(Guid? paymentMethodId)
    {
        if (paymentMethodId.HasValue)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(paymentMethodId.Value);
            if (paymentMethod == null)
            {
                throw new PaymentMethodNotFoundException(paymentMethodId.Value);    
            }

            return paymentMethod.Name;
        }

        return null;
    }
}