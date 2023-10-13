using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Application.Dtos.Materials;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.Exceptions;
using Products.Domain.MaterialAggregate.Specifications;

namespace Products.Application.Queries.MaterialQuery.GetMaterialById;

public class GetMaterialByIdQueryHandler : IQueryHandler<GetMaterialByIdQuery, GetMaterialDto>
{
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IMapper _mapper;
    
    public GetMaterialByIdQueryHandler(
        IReadOnlyRepository<Material> materialRepository,
        IMapper mapper)
    {
        _materialRepository = materialRepository;
        _mapper = mapper;
    }
    
    public async Task<GetMaterialDto> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.FindAsync(new MaterialByIdSpecification(request.Id));
        if (material == null)
        {
            throw new MaterialNotFoundException(request.Id);
        }

        return _mapper.Map<GetMaterialDto>(material);
    }
}