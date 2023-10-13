using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Queries.ProductQuery.GetAllProductType;

public class GetAllProductTypeQuery : IQuery<IEnumerable<ProductTypeDto>>
{
    
}