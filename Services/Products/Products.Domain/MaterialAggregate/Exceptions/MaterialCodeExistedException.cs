using BuildingBlocks.Domain.Exceptions.Resource;

namespace Products.Domain.MaterialAggregate.Exceptions;

public class MaterialCodeExistedException : ResourceAlreadyExistException
{
    public MaterialCodeExistedException(string materialCode) : base($"Material code '{materialCode}' is existed", ErrorCodes.DuplicateMaterialCode)
    {
    }
}