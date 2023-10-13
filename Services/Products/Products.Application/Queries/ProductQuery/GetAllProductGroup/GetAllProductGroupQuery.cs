using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Queries.ProductQuery.GetAllProductGroup;

public class GetAllProductGroupQuery : IQuery<IEnumerable<ProductGroupDto>>
{
    
}