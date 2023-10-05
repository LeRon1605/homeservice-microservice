using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractLineNotFoundException : ResourceNotFoundException
{
    public ContractLineNotFoundException(Guid id) 
        : base(nameof(ContractLine), id, ErrorCodes.ContractLineNotFound)
    {
    }
}