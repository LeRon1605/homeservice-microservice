using AutoMapper;
using Products.Application.Dtos;
using Products.Application.Dtos.Materials;
using Products.Application.Dtos.Products;
using Products.Domain.MaterialAggregate;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Product, GetProductDto>()
            .ForMember(dest => dest.Colors, options => 
                options.MapFrom(src => string.IsNullOrEmpty(src.Colors) ? Array.Empty<string>() : src.Colors.Split(',', StringSplitOptions.None)))
            .ForMember(dest => dest.Group,
                options => options.MapFrom(src => new ProductGroupDto()
                    { Id = src.ProductGroupId, Name = src.Group.Name }))
            .ForMember(dest => dest.Type,
                options => options.MapFrom(src => new ProductTypeDto()
                    { Id = src.ProductTypeId, Name = src.Type.Name }))
            .ForMember(dest => dest.BuyUnit,
                options => options.MapFrom(src =>
                    src.BuyUnitId == null
                        ? null
                        : new ProductUnitDto { Id = src.BuyUnitId!.Value, Name = src.BuyUnit.Name }))
            .ForMember(dest => dest.SellUnit,
                options => options.MapFrom(src =>
                    src.SellUnitId == null
                        ? null
                        : new ProductUnitDto { Id = src.SellUnitId!.Value, Name = src.SellUnit.Name }));

        CreateMap<ProductGroup, ProductGroupDto>();

        CreateMap<ProductType, ProductTypeDto>();

        CreateMap<ProductUnit, ProductUnitDto>();

        CreateMap<ProductImage, ProductImageDto>();

        CreateMap<Material, GetMaterialDto>();
    } 
}