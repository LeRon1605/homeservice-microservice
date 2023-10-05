using AutoMapper;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Customers;
using Contracts.Application.Dtos.Taxes;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.TaxAggregate;

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

        CreateMap<InstallationAddress, InstallationAddressDto>();

        CreateMap<Contract, ContractDto>()
            .ForMember(dest => dest.CustomerName, options => options.MapFrom(src => src.Customer!.Name));
        CreateMap<Contract, ContractDetailDto>();
        CreateMap<ContractLine, ContractLineDto>();
        
        CreateMap<Tax, TaxDto>();
    } 
}