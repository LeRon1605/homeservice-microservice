using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Customers.Application.Dtos;
using Customers.Domain.CustomerAggregate;
using Customers.Domain.Exceptions;

namespace Customers.Application.Queries;

public class CustomerByIdQueryHandler : IQueryHandler<CustomerByIdQuery, CustomerDto>
{
    private readonly IReadOnlyRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomerByIdQueryHandler(IReadOnlyRepository<Customer> customerRepository,
                                    IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CustomerByIdQuery request,
                                           CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id) 
                       ?? throw new CustomerNotFoundException(request.Id);
        return _mapper.Map<CustomerDto>(customer);
    }
}