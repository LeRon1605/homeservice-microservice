using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Installations.Application.Dtos;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Specifications;

namespace Installations.Application.Queries;

public class GetInstallationsByContractIdQueryHandler : IQueryHandler<GetInstallationsByContractIdQuery, PagedResult<InstallationInContractDto>>
{
    private readonly IReadOnlyRepository<Installation> _installationRepository;
    private readonly IMapper _mapper;

    public GetInstallationsByContractIdQueryHandler(IReadOnlyRepository<Installation> installationRepository,
                                                    IMapper mapper)
    {
        _installationRepository = installationRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<InstallationInContractDto>> Handle(GetInstallationsByContractIdQuery request,
                                                     CancellationToken cancellationToken)
    {
        var spec = new InstallationsByContractIdSpecification(request.ContractId, request.Search, request.PageSize,
            request.PageIndex);
        var (installations, totalCount) = await _installationRepository.FindWithTotalCountAsync(spec);
        
        var installationsDto = _mapper.Map<IEnumerable<InstallationInContractDto>>(installations);

        return new PagedResult<InstallationInContractDto>(installationsDto, totalCount, request.PageIndex, request.PageSize);
    }
}