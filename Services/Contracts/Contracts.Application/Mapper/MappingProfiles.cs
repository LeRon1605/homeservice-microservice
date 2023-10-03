using AutoMapper;
using Contracts.Application.Dtos.Customers;
using Contracts.Domain.CustomerAggregate;

namespace Contracts.Application.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address.FullAddress))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, options => options.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.PostalCode, options => options.MapFrom(src => src.Address.PostalCode));
    } 
}