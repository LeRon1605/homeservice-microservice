using AutoMapper;

namespace Products.Application.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Domain.ProductAggregate.Product, Dtos.GetProductDto>();
    } 
}