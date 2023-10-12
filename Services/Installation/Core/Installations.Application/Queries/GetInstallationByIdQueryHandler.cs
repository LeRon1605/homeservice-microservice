using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Application.Dtos;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Exceptions;
using Installations.Domain.InstallationAggregate.Specifications;

namespace Installations.Application.Queries;

public class GetInstallationByIdQueryHandler : IQueryHandler<GetInstallationByIdQuery, InstallationDetailDto>
{
    private readonly IReadOnlyRepository<Installation> _installationRepository;
    private readonly IMapper _mapper;

    public GetInstallationByIdQueryHandler(IReadOnlyRepository<Installation> installationRepository,
                                           IMapper mapper)
    {
        _installationRepository = installationRepository;
        _mapper = mapper;
    }

    public async Task<InstallationDetailDto> Handle(GetInstallationByIdQuery request, CancellationToken cancellationToken)
    {
        var installation = await _installationRepository.FindAsync(new InstallationByIdSpecification(request.InstallationId));
        if (installation is null)
            throw new InstallationNotFoundException(request.InstallationId);

        return _mapper.Map<InstallationDetailDto>(installation); 
    }
}