using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.MaterialAggregate.Specifications;

public class MaterialCodeDuplicatedSpecification : Specification<Material>
{
    private readonly string _materialCode;
    public override Expression<Func<Material, bool>> ToExpression()
    {
        return m => m.MaterialCode == _materialCode;
    }

    public MaterialCodeDuplicatedSpecification(string materialCode)
    {
        _materialCode = materialCode;
    }
}