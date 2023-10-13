using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;
using Products.Application.Dtos.Materials;

namespace Products.Application.Commands.MaterialCommands.AddMaterial;

public class AddMaterialCommand : MaterialCreateDto, ICommand<GetMaterialDto>
{
    public AddMaterialCommand(string materialCode,
                              string name,
                              Guid typeId,
                              Guid? sellUnitId,
                              decimal? sellPrice,
                              decimal? cost,
                              bool isObsolete)
    {
        MaterialCode = materialCode;
        Name = name;
        TypeId = typeId;
        SellUnitId = sellUnitId;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
}