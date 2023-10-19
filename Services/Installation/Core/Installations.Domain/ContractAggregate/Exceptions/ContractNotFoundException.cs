using BuildingBlocks.Domain.Exceptions.Resource;

namespace Installations.Domain.ContractAggregate.Exceptions;

public class ContractNotFoundException : ResourceNotFoundException
{
    public ContractNotFoundException(Guid contractId) : base(nameof(Contract), contractId, ErrorCodes.ContractNotFound)
    {
    } 
}