using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Queries.ProductQuery.GetAllProductType;

public class GetAllProductTypeQuery : IQuery<IEnumerable<ProductTypeDto>>
{
    
}