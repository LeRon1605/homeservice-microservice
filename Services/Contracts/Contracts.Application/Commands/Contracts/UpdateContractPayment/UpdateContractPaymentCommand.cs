using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractUpdate;

namespace Contracts.Application.Commands.Contracts.UpdateContractPayment;

public class UpdateContractPaymentCommand : ContractPaymentUpdateDto, ICommand<ContractPaymentDto>
{
    public Guid ContractId { get; set; }
    public Guid PaymentId { get; set; }
    
    public UpdateContractPaymentCommand(Guid contractId, Guid paymentId, ContractPaymentUpdateDto dto)
    {
        ContractId = contractId;
        PaymentId = paymentId;
        DatePaid = dto.DatePaid;
        PaidAmount = dto.PaidAmount;
        Surcharge = dto.Surcharge;
        Reference = dto.Reference;
        Comments = dto.Comments;
        PaymentMethodId = dto.PaymentMethodId;
    }
}