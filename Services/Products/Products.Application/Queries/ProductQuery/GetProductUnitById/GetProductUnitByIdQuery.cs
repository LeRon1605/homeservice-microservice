using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos.Products;

namespace Products.Application.Queries.ProductQuery.GetProductUnitById;

public class GetProductUnitByIdQuery : IQuery<ProductUnitDto>
{
    public Guid ProductUnitId { get; set; }
    
    public GetProductUnitByIdQuery(Guid productUnitId)
    {
        ProductUnitId = productUnitId;
    }
}