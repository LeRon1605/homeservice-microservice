using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Queries.ProductQuery.GetAllProductByIncludedIds;

public class GetAllProductByIncludedIdsQuery : IQuery<IEnumerable<GetProductDto>>
{
    public IEnumerable<Guid> Ids { get; set; }
    
    public GetAllProductByIncludedIdsQuery(IEnumerable<Guid> ids)
    {
        Ids = ids;
    }
}