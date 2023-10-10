using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractLineEmptyException : ResourceInvalidOperationException
{
    public ContractLineEmptyException() 
        : base("Contract should have at least one contract line!", ErrorCodes.ContractLineEmpty)
    {
    }
}