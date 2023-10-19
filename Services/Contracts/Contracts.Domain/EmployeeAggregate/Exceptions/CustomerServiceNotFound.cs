using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.EmployeeAggregate.Exceptions;

public class CustomerServiceNotFound : ResourceNotFoundException
{
    public CustomerServiceNotFound(Guid id) 
        : base($"Customer service with Id '{id}' does not exist!", ErrorCodes.EmployeeNotFound)
    {
        
    }
}