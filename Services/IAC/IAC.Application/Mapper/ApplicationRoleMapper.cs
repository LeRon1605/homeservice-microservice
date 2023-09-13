using AutoMapper;
using IAC.Application.Dtos.Roles;
using IAC.Domain.Entities;

namespace IAC.Application.Mapper;

public class ApplicationRoleMapper : Profile
{
    public ApplicationRoleMapper()
    {
        CreateMap<ApplicationRole, RoleDto>();
    }
}