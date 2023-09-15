using AutoMapper;
using Customers.Application.Dtos;
using Customers.Domain.CustomerAggregate;

namespace Customers.Application.Mapper;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address.FullAddress))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, options => options.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.PostalCode, options => options.MapFrom(src => src.Address.PostalCode));
    }
}