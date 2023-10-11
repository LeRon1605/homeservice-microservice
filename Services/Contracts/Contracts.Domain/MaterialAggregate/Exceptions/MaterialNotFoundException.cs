using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.MaterialAggregate.Exceptions;

public class MaterialNotFoundException : ResourceNotFoundException
{
    public MaterialNotFoundException(Guid id) : base(nameof(Material), id, ErrorCodes.MaterialNotFound)
    {
    } 
}