using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.EmployeeAggregate.Exceptions;

public class SupervisorNotFoundException : ResourceNotFoundException
{
    public SupervisorNotFoundException(Guid id) 
        : base($"Supervisor with Id '{id}' does not exist!", ErrorCodes.EmployeeNotFound)
    {
            
    }
}