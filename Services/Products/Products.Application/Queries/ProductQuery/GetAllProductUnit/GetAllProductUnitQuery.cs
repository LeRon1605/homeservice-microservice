using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Queries.ProductQuery.GetAllProductUnit;

public class GetAllProductUnitQuery : IQuery<IEnumerable<ProductUnitDto>>
{
}