using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractActionNotFoundException : ResourceNotFoundException
{
    public ContractActionNotFoundException(Guid id) 
        : base(nameof(ContractAction), id, ErrorCodes.ContractActionNotFound)
    {
    }
}