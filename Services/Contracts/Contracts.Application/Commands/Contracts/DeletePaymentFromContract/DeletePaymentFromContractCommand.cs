using BuildingBlocks.Application.CQRS;

namespace Contracts.Application.Commands.Contracts.DeletePaymentFromContract;

public class DeletePaymentFromContractCommand : ICommand
{
    public Guid ContractId { get; set; }
    public Guid PaymentId { get; set; }
    
    public DeletePaymentFromContractCommand(Guid contractId, Guid paymentId)
    {
        ContractId = contractId;
        PaymentId = paymentId;
    }
}