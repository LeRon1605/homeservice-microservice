using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Installations.Application.Dtos;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Specifications;

namespace Installations.Application.Queries;

public class GetAllInstallationQueryHandler : IQueryHandler<GetAllInstallationsQuery, PagedResult<InstallationDto>>
{
    private readonly IReadOnlyRepository<Installation> _installationRepository;
    private readonly IMapper _mapper;
    
    public GetAllInstallationQueryHandler(
        IReadOnlyRepository<Installation> installationRepository,
        IMapper mapper)
    {
        _installationRepository = installationRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<InstallationDto>> Handle(GetAllInstallationsQuery request, CancellationToken cancellationToken)
    {
        var specification = new InstallationFilterSpecification(
            request.Status,
            request.FromInstallDate,
            request.ToInstallDate,
            request.ProductId,
            request.InstallerId,
            request.Search,
            request.PageIndex,
            request.PageSize);
        
        var (installations, totalCount) = await _installationRepository.FindWithTotalCountAsync(specification);
        return new PagedResult<InstallationDto>(
            _mapper.Map<IEnumerable<InstallationDto>>(installations), 
            totalCount, 
            request.PageIndex, 
            request.PageSize);
    }
}