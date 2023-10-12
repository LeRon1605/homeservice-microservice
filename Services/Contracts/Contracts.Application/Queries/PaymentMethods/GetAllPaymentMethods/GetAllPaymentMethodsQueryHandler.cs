using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.PaymentMethods;
using Contracts.Domain.PaymentMethodAggregate;

namespace Contracts.Application.Queries.PaymentMethods.GetAllPaymentMethods;

public class GetAllPaymentMethodsQueryHandler : IQueryHandler<GetAllPaymentMethodsQuery, IEnumerable<PaymentMethodDto>>
{
    private readonly IReadOnlyRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IMapper _mapper;
    
    public GetAllPaymentMethodsQueryHandler(
        IReadOnlyRepository<PaymentMethod> paymentMethodRepository,
        IMapper mapper)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PaymentMethodDto>> Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        var paymentMethods = await _paymentMethodRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);
    }
}