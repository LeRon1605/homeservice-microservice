using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractPaymentNotFoundException : ResourceNotFoundException
{
    public ContractPaymentNotFoundException(Guid id) 
        : base(nameof(ContractPayment), id, ErrorCodes.ContractPaymentNotFound)
    {
    }
}