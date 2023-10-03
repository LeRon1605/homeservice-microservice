using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.ContractAggregate.Exceptions;

public class ContractLineExistedException : ResourceAlreadyExistException
{
    public ContractLineExistedException(Guid productId, Guid unitId, string? color) 
        : base($"Item has already added to contract: ProductId: {productId}, UnitId: {unitId}, Color: {color}", ErrorCodes.ContractLineExisted)
    {
        
    }
}