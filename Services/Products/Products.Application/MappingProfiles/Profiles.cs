using AutoMapper;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;

namespace Products.Application.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Buy, options => options.MapFrom(src => new ProductPriceDto() { Price = src.BuyPrice, Unit = new ProductUnitDto() { Id = src.BuyUnitId, Name = src.BuyUnit.Name } }))
            .ForMember(dest => dest.Sell, options => options.MapFrom(src => new ProductPriceDto() { Price = src.SellPrice, Unit = new ProductUnitDto() { Id = src.SellUnitId, Name = src.SellUnit.Name } }))
            .ForMember(dest => dest.Group, options => options.MapFrom(src => new ProductGroupDto() { Id = src.ProductGroupId, Name = src.Group.Name }))
            .ForMember(dest => dest.Type, options => options.MapFrom(src => new ProductTypeDto() { Id = src.ProductTypeId, Name = src.Type.Name }));

    } 
}