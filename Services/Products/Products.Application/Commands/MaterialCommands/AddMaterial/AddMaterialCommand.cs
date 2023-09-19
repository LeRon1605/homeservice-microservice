using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.MaterialCommands.AddMaterial;

public class AddMaterialCommand : MaterialCreateDto, ICommand<GetMaterialDto>
{
    public AddMaterialCommand(string materialCode,
                              string name,
                              Guid productTypeId,
                              Guid? sellUnitId,
                              decimal? sellPrice,
                              decimal? cost,
                              bool isObsolete)
    {
        MaterialCode = materialCode;
        Name = name;
        ProductTypeId = productTypeId;
        SellUnitId = sellUnitId;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
}