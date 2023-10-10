using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.PaymentMethods;

namespace Contracts.Application.Queries.PaymentMethods.GetAllPaymentMethods;

public class GetAllPaymentMethodsQuery : IQuery<IEnumerable<PaymentMethodDto>>
{
    
}