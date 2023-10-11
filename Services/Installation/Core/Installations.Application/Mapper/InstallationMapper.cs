using AutoMapper;
using Installations.Application.Dtos;
using Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;

namespace Installations.Application.Mapper;

public class InstallationMapper : Profile
{
    public InstallationMapper()
    {
        CreateMap<InstallationItemEventDto, InstallationItemCreateDto>();
        CreateMap<InstallationAddressEventDto, InstallationAddressDto>();
    }
}