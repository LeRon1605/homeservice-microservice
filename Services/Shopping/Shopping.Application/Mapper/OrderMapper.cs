using AutoMapper;
using Shopping.Application.Dtos;
using Shopping.Application.Dtos.Buyers;
using Shopping.Application.Dtos.Orders;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Mapper;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactInfo.ContactName))
            .ForMember(dest => dest.OrderNo, opt => opt.MapFrom(src => src.OrderNo))
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactInfo.ContactName))
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.BuyerId))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactInfo.Phone))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.ContactInfo.Email))
            .ForMember(dest => dest.OrderValue, opt => opt.MapFrom(src => src.OrderLines.Sum(x=>x.Quantity * x.Cost)))
            .ForMember(dest => dest.PlacedDate, opt => opt.MapFrom(src => src.PlacedDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        
        CreateMap<Buyer, BuyerDto>()
            .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address.FullAddress))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, options => options.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.PostalCode, options => options.MapFrom(src => src.Address.PostalCode));

        //CreateMap<OrderContactInfo, OrderDetailsDto>();
        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UnitName))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Quantity * src.Cost));

        CreateMap<Order, OrderDetailsDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.ContactInfo.CustomerName))
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactInfo.ContactName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ContactInfo.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.ContactInfo.Phone))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ContactInfo.Address))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ContactInfo.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.ContactInfo.State))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ContactInfo.PostalCode))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderLines))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status));
    }
}