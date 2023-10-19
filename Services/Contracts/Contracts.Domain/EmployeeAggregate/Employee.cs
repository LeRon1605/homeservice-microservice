using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.EmployeeAggregate;

public class Employee : AggregateRoot
{
    public string Name { get; set; }
    public string Role { get; set; }
    
    public Employee(Guid id, string name, string role)
    {
        Id = id;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        Role = Guard.Against.NullOrWhiteSpace(role, nameof(Role));
    }

    public void Update(string name, string role)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        Role = Guard.Against.NullOrWhiteSpace(role, nameof(Role));
    }
    
    private Employee()
    {
    }
}