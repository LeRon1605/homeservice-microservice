using AutoMapper;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;
using Contracts.Application.Dtos.Contracts.ContractUpdate;
using Contracts.Application.Dtos.Customers;
using Contracts.Application.Dtos.PaymentMethods;
using Contracts.Application.Dtos.Taxes;
using Contracts.Application.IntegrationEvents.Events.Contracts.EventDtos;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.PaymentMethodAggregate;
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
            .ForMember(dest => dest.CustomerName, options => options.MapFrom(src => src.Customer!.Name))
            .ForMember(dest => dest.ContractValue, options => options.MapFrom(src => src.Items.Sum(x => x.Quantity * x.SellPrice)))
            .ForMember(dest => dest.SalePersonName, options => options.MapFrom(src => src.SalePerson!.Name));
        CreateMap<Contract, ContractDetailDto>()
            .ForMember(dest => dest.ContractValue, options => options.MapFrom(src => src.Items.Sum(x => x.Quantity * x.SellPrice)))
            .ForMember(dest => dest.SalePerson, options => options.MapFrom(src => new EmployeeInContractDto()
            {
                Id = src.SalePersonId,
            }))
            .ForMember(dest => dest.Supervisor, options => options.MapFrom(src => src.SupervisorId.HasValue
                ? new EmployeeInContractDto()
                {
                    Id = src.SupervisorId.Value,
                }
                : null))
            .ForMember(dest => dest.CustomerServiceRep, options => options.MapFrom(src => src.CustomerServiceRepId.HasValue
                ? new EmployeeInContractDto()
                {
                    Id = src.CustomerServiceRepId.Value,
                }
                : null));
        
        CreateMap<ContractLine, ContractLineDto>()
            .ForMember(dest => dest.Product,
                options => options.MapFrom(src => new ProductInContractLineDto()
                    { Id = src.ProductId, Name = src.ProductName }))
            .ForMember(dest => dest.ProductUnit,
                options => options.MapFrom(src => new ProductUnitInContractLineDto()
                    { Id = src.UnitId, Name = src.UnitName }))
            .ForMember(dest => dest.Tax,
                options => options.MapFrom(src => new TaxInContractLineDto()
                    { Id = src.TaxId, Name = src.TaxName, Value = src.TaxValue }));

        CreateMap<Contract, ContractsOfCustomerDto>()
            .ForMember(dest => dest.ContractNo, opt => opt.MapFrom(src => src.No))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest =>dest.ContractValue, opt => opt.MapFrom(src=> src.Items.Sum(x=>x.Quantity * x.SellPrice)))
            .ForMember(dest => dest.QuotedAt, opt => opt.MapFrom(src => src.QuotedAt))
            .ForMember(dest => dest.SoldAt, opt => opt.MapFrom(src => src.SoldAt))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        CreateMap<ContractLine, ContractLineEventDto>();
        
        CreateMap<Tax, TaxDto>();
        CreateMap<ContractPayment, ContractPaymentDto>()
            .ForMember(dest => dest.PaymentMethod,
                options => options.MapFrom(src => new PaymentMethodInContractPaymentDto()
                    { Id = src.PaymentMethodId, Name = src.PaymentMethodName }));

        CreateMap<ContractAction, ContractActionDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.CreatedBy) ? "System" : src.CreatedBy))
            .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => new EmployeeInContractActionDto()
            {
                Id = src.ActionByEmployeeId,
                Name = src.ActionByEmployee!.Name,
            }));
        
        
        CreateMap<PaymentMethod, PaymentMethodDto>();

        CreateMap<InstallationAddressDto, InstallationAddressEventDto>();
        CreateMap<InstallationItemCreateDto, InstallationItemEventDto>();
        CreateMap<InstallationCreateDto, InstallationEventDto>();

        CreateMap<InstallationUpdateDto, InstallationUpdatedEventDto>();
        CreateMap<InstallationItemUpdateDto, InstallationItemEventDto>();
    } 
}