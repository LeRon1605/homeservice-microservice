using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractNotFoundException : ResourceNotFoundException
{
    public ContractNotFoundException(Guid id) : base($"Contracts with customer id: {id} not found",
        ErrorCodes.ContractNotFoundException)
    {
    }
}