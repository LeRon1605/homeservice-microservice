using BuildingBlocks.Application.CQRS;
using Customers.Application.Dtos;

namespace Customers.Application.Queries;

public class CustomerByIdQuery : IQuery<CustomerDto?>
{
    public Guid Id { get; private set; }
    public CustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}