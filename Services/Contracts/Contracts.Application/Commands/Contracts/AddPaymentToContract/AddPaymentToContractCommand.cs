using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts.ContractCreate;

namespace Contracts.Application.Commands.Contracts.AddPaymentToContract;

public class AddPaymentToContractCommand : ContractPaymentCreateDto, ICommand
{
    public Guid ContractId { get; set; }

    public AddPaymentToContractCommand(Guid id, ContractPaymentCreateDto dto)
    {
        ContractId = id;
        DatePaid = dto.DatePaid;
        PaidAmount = dto.PaidAmount;
        Surcharge = dto.Surcharge;
        Reference = dto.Reference;
        Comments = dto.Comments;
        PaymentMethodId = dto.PaymentMethodId;
    }
}