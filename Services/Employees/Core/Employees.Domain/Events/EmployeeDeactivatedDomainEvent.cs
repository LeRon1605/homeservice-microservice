using BuildingBlocks.Domain.Event;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Domain.Events;

public class EmployeeDeactivatedDomainEvent  : IDomainEvent
{
    public EmployeeDeactivatedDomainEvent(Employee employee)
    {
        Employee = employee;
    }

    public Employee Employee { get; set; }
}