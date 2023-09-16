using AutoMapper;
using IAC.Application.Dtos.Users;
using IAC.Domain.Entities;

namespace IAC.Application.Mapper;

public class ApplicationUserMapper : Profile
{
    public ApplicationUserMapper()
    {
        CreateMap<ApplicationUser, UserDto>();
    }
}