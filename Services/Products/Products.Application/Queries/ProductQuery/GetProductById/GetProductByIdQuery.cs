using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;

namespace Products.Application.Queries.ProductQuery.GetProductById;

public class GetProductByIdQuery : IQuery<GetProductDto>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}