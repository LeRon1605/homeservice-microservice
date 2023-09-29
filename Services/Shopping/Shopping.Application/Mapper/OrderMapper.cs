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
        CreateMap<Order, OrderDto>();
        
        CreateMap<Buyer, BuyerDto>()
            .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address.FullAddress))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, options => options.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.PostalCode, options => options.MapFrom(src => src.Address.PostalCode));
    }
}