using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.Specifications;

namespace Products.Application.Queries.MaterialQuery.GetMaterialWithPagination;

public class GetMaterialsWithPaginationQueryHandler: IQueryHandler<GetMaterialsWithPaginationQuery, PagedResult<GetMaterialDto>>
{
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IMapper _mapper;
    public GetMaterialsWithPaginationQueryHandler(IReadOnlyRepository<Material> materialRepository, IMapper mapper)
    {
        _materialRepository = materialRepository;
        _mapper = mapper;
    }
    public async Task<PagedResult<GetMaterialDto>> Handle(GetMaterialsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var getMaterialSpecification = new MaterialsWithPaginationSpecification(request.Search, request.PageIndex, request.PageSize, 
            request.IsObsolete, request.TypeId);
        var (materials, totalCount) = await _materialRepository.FindWithTotalCountAsync(getMaterialSpecification);
        var materialsDto = _mapper.Map<IEnumerable<GetMaterialDto>>(materials);
        return new PagedResult<GetMaterialDto>(materialsDto, totalCount, request.PageIndex, request.PageSize);
    }
}