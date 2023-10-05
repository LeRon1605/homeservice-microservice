using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.TaxAggregate.Exceptions;

public class TaxNotFoundException : ResourceNotFoundException
{
    public TaxNotFoundException(Guid id) : base(nameof(Tax), id, ErrorCodes.TaxNotFound)
    {
    }
}