using Customers.Application.Dtos;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
namespace Customers.Application.Queries;

public class CustomerFilterAndPagingQuery : PagingParameters, IQuery<PagedResult<CustomerDto>>
{
}