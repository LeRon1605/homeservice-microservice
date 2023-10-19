using BuildingBlocks.Domain.Exceptions.Resource;

namespace Installations.Domain.ContractAggregate.Exceptions;

public class ContractLineNotFoundException : ResourceNotFoundException
{
    public ContractLineNotFoundException(Guid contractLineId)
        : base(nameof(ContractLine), contractLineId, ErrorCodes.ContractLineNotFound)
    {
    } 
}