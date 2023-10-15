using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.TaxAggregate;

public class Tax : AuditableAggregateRoot
{
    public string Name { get; private set; }
    public double Value { get; private set; }

    public Tax(string name, double value)
    {
        Name = Guard.Against.NullOrWhiteSpace(name);
        Value = Guard.Against.Negative(value, nameof(Value));
    }
}