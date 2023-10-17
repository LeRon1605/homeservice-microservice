using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.PaymentMethodAggregate;
using Contracts.Domain.PaymentMethodAggregate.Exceptions;

namespace Contracts.Application.Commands.Contracts.UpdateContractPayment;

public class UpdateContractPaymentCommandHandler : ICommandHandler<UpdateContractPaymentCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateContractPaymentCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<PaymentMethod> paymentMethodRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateContractPaymentCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        contract.UpdatePayment(
            request.PaymentId, 
            request.DatePaid, 
            request.PaidAmount, 
            request.Surcharge, 
            request.Reference, 
            request.Comments, 
            request.PaymentMethodId, 
            await GetPaymentMethodNameByIdAsync(request.PaymentMethodId));

        await _unitOfWork.SaveChangesAsync();
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