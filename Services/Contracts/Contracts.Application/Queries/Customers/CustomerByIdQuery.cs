using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Customers;

namespace Contracts.Application.Queries.Customers;

public class CustomerByIdQuery : IQuery<CustomerDto>
{
    public Guid Id { get; private set; }
    public CustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}