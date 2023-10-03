using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Contracts.Application.Dtos.Customers;

namespace Contracts.Application.Queries.Customers;

public class CustomerFilterAndPagingQuery : PagingParameters, IQuery<PagedResult<CustomerDto>>
{
}