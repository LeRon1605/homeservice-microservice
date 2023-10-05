using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractNotFoundException : ResourceNotFoundException
{
    public ContractNotFoundException(Guid id) 
        : base(nameof(Contract), id, ErrorCodes.ContractNotFound)
    {
    }
}