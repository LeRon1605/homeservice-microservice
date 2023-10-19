using BuildingBlocks.Domain.Event;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Domain.Events;

public class EmployeeAddedDomainEvent : IDomainEvent
{
    public Employee Employee { get; set; }
    
    public EmployeeAddedDomainEvent(Employee employee)
    {
        Employee = employee;
    }
}