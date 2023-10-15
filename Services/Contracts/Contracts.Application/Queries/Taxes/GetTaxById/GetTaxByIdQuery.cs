using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Taxes;

namespace Contracts.Application.Queries.Taxes.GetTaxById;

public class GetTaxByIdQuery : IQuery<TaxDto>
{
    public Guid TaxId { get; set; }
    
    public GetTaxByIdQuery(Guid taxId)
    {
        TaxId = taxId;
    }
}