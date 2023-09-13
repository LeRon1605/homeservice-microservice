using AutoMapper;
using Shopping.Application.Dtos;
using Shopping.Domain.ShoppingAggregate;

namespace Shopping.Application.Mapper;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderFilterAndPagingDto>();
    }

}