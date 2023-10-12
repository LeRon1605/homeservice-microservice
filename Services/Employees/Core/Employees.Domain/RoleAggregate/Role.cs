using BuildingBlocks.Domain.Models;

namespace Employees.Domain.RoleAggregate;

public class Role : AggregateRoot
{
    public string Id { get; init; }
    public string Name { get; set; }

    public Role(string id, string name)
    {
        Id = id;
        Name = name;
    }
}