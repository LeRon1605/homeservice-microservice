using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Queries.ProductQuery.GetAllProductGroup;

public class GetAllProductGroupQuery : IQuery<IEnumerable<ProductGroupDto>>
{
    
}