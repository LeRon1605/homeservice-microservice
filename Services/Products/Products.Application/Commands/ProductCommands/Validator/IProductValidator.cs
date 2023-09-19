namespace Products.Application.Commands.ProductCommands.Validator;

public interface IProductValidator
{
    Task CheckProductTypeExistAsync(Guid id);
    Task CheckProductGroupExistAsync(Guid id);
    Task CheckProductUnitExistAsync(Guid buyUnitId, Guid sellUnitId);
}