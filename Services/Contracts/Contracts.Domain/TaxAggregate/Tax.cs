using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.TaxAggregate;

public class Tax : AggregateRoot
{
    public string Name { get; set; }

    public Tax(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name);
    }
}