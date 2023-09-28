using BuildingBlocks.Application.CQRS;
using Shopping.Application.Dtos;

namespace Shopping.Application.Queries.BuyerQueries;

public class BuyerByIdQuery : IQuery<BuyerDto>
{
    public Guid Id { get; private set; }
    public BuyerByIdQuery(Guid id)
    {
        Id = id;
    }
}