using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.MaterialAggregate;

public class Material : AuditableAggregateRoot
{
    public string Name { get; private set; } = null!;
    public bool IsObsolete { get; private set; }
    
    public Material(Guid id, string name, bool isObsolete)
    {
        Id = id;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        IsObsolete = isObsolete;
    }
    
    private Material() { }
    
    public void Update(string name, bool isObsolete)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        IsObsolete = isObsolete;
    }
}