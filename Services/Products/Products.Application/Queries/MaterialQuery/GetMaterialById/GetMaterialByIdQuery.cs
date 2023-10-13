using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Materials;

namespace Products.Application.Queries.MaterialQuery.GetMaterialById;

public class GetMaterialByIdQuery : IQuery<GetMaterialDto>
{
    public Guid Id { get; set; }
    
    public GetMaterialByIdQuery(Guid id)
    {
        Id = id;
    }
}