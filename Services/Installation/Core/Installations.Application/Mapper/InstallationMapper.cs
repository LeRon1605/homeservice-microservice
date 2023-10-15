using AutoMapper;
using Installations.Application.Dtos;
using Installations.Application.IntegrationEvents.Events.Contracts.EventDtos;
using Installations.Domain.InstallationAggregate;

namespace Installations.Application.Mapper;

public class InstallationMapper : Profile
{
    public InstallationMapper()
    {
        CreateMap<InstallationItemEventDto, InstallationItemCreateDto>();
        CreateMap<InstallationItemEventDto, InstallationItemUpdateDto>();
        CreateMap<InstallationAddressEventDto, InstallationAddressDto>();

        CreateMap<Installation, InstallationDto>()
            .ForMember(dto => dto.Installer, opt => opt.MapFrom(i => new InstallerDto { Id = i.InstallerId }))
            .ForMember(dto => dto.EstimatedHours, opt => opt
                .MapFrom(i =>
                    (i.EstimatedStartTime != null && i.EstimatedFinishTime != null) 
                        ? (i.EstimatedFinishTime.Value - i.EstimatedStartTime.Value).TotalHours 
                        : 0))
            .ForMember(dto => dto.ActualHours, opt => opt
                .MapFrom(i =>
                    (i.ActualStartTime != null && i.ActualFinishTime != null) 
                        ? (i.ActualFinishTime.Value - i.ActualStartTime.Value).TotalHours 
                        : 0));

        CreateMap<Installation, InstallationDetailDto>();
        CreateMap<InstallationItem, InstallationItemDto>();
    }
}