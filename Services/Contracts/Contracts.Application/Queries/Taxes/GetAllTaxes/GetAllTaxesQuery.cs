using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Taxes;

namespace Contracts.Application.Queries.Taxes.GetAllTaxes;

public class GetAllTaxesQuery : IQuery<IEnumerable<TaxDto>>
{
    
}