using ApiGateway.Dtos.Products;
using AutoMapper;

namespace ApiGateway.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<GetProductDto, ProductData>();
    } 
}