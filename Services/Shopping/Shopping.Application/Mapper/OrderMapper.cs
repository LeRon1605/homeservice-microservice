using AutoMapper;
using Shopping.Application.Dtos;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Mapper;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderDto>();
    }
}