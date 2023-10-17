using BuildingBlocks.Domain.Event;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Domain.Events;

public class EmployeeUpdatedDomainEvent : IDomainEvent
{
    public Employee Employee { get; set; }

    public EmployeeUpdatedDomainEvent(Employee employee)
    {
        Employee = employee;
    }
}