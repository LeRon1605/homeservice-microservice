using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.EmployeeAggregate.Exceptions;

public class SalePersonNotFoundException : ResourceNotFoundException
{
    public SalePersonNotFoundException(Guid id) 
        : base($"Sale person with Id '{id}' does not exist!", ErrorCodes.EmployeeNotFound)
    {
        
    }
}