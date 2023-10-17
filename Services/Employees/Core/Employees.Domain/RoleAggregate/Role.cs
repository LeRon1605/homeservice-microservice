using BuildingBlocks.Domain.Models;

namespace Employees.Domain.RoleAggregate;

public class Role : AggregateRoot
{
    public string Name { get; set; }

    public Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}