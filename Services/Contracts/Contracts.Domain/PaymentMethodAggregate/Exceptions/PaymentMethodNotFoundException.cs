using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.PaymentMethodAggregate.Exceptions;

public class PaymentMethodNotFoundException : ResourceNotFoundException
{
    public PaymentMethodNotFoundException(Guid id) 
        : base(nameof(PaymentMethod), id, ErrorCodes.PaymentMethodNotFound)
    {
    }
}