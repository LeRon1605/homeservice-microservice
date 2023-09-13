using AutoMapper;
using Customers.Application.Dtos;
using Customers.Domain.CustomerAggregate;

namespace Customers.Application.Mapper;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, CustomerFilterAndPagingDto>();
    }

}