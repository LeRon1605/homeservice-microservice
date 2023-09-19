using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Queries.ProductQuery.GetAllProductUnit;

public class GetAllProductUnitQuery : IQuery<IEnumerable<ProductUnitDto>>
{
}