using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.PaymentMethodAggregate;

public class PaymentMethod : AggregateRoot
{
    public string Name { get; set; }
    
    public PaymentMethod(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
    }

    private PaymentMethod()
    {
        
    }        
}